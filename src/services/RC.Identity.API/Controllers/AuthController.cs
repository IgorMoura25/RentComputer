using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RC.Core.Messages.IntegrationEvents;
using RC.Identity.API.Configurations;
using RC.Identity.API.CryptoHandlers;
using RC.Identity.API.Models;
using RC.MessageBus.EasyNetQ;
using RC.WebAPI.Core;

namespace RC.Identity.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ICryptoHandler _cryptoHandler;
        private readonly IOptions<JwtConfigurationOptions> _options;
        private readonly IEasyNetQBus _messageBus;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            ICryptoHandler cryptoHandler,
            IOptions<JwtConfigurationOptions> options,
            IEasyNetQBus messageBus,
            ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cryptoHandler = cryptoHandler;
            _options = options;
            _messageBus = messageBus;
            _logger = logger;
        }

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded && model.Email != null)
            {
                var addedUser = await _userManager.FindByNameAsync(model.Email);

                _logger.LogInformation("Starting RPC with others API");

                // Conversa com outras API's avisando que o usuário foi criado
                var integrationResponse = await _messageBus.RequestIntegrationAsync<UserCreatedIntegrationEvent, ResponseIntegrationMessage>
                    (new UserCreatedIntegrationEvent(Guid.Parse(addedUser.Id), addedUser.UserName, addedUser.Email, "57465830060", true, DateTime.UtcNow));

                // Se deu algum erro com a criação do cliente, deleta usuário
                if (!integrationResponse?.ValidationResult?.IsValid ?? false)
                {
                    _logger.LogInformation("RPC failed");

                    await _userManager.DeleteAsync(user);

                    if (integrationResponse?.ValidationResult?.Errors?.Count > 0)
                    {
                        foreach (var error in integrationResponse.ValidationResult.Errors)
                        {
                            AddError(error.ErrorMessage);
                        }

                        return CustomResponse();
                    }
                }

                _logger.LogInformation("RPC success");

                await _signInManager.SignInAsync(user, isPersistent: false);

                var accessToken = await GenerateJwt(model.Email);
                var refreshToken = await _cryptoHandler.CreateRefreshTokenAsync(model.Email);

                var response = new
                {
                    access_token = accessToken,
                    refreshToken = refreshToken.Token
                };

                return CustomResponse(response);
            }

            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }

            return CustomResponse();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded && model.Email != null)
            {
                var accessToken = await GenerateJwt(model.Email);
                var refreshToken = await _cryptoHandler.CreateRefreshTokenAsync(model.Email);
                var jwtRefreshToken = await _cryptoHandler.CreateJwtRefreshTokenAsync(_options.Value.Issuer, await GetJwtClaims(model.Email), DateTime.UtcNow.AddMinutes(480)); // 8 horas

                var response = new
                {
                    access_token = accessToken,
                    refreshToken = refreshToken.Token,
                    jwtRefreshToken = jwtRefreshToken
                };

                return CustomResponse(response);
            }

            if (result.IsLockedOut)
            {
                AddError("User temporary blocked for invalid attempts");
                return CustomResponse();
            }

            AddError("The user or password is incorrect");

            return CustomResponse();
        }

        [HttpGet]
        [Route("validate-cc")]
        public async Task<IActionResult> ValidateCreditCardAsync([FromQuery] string jwe)
        {
            var result = await _cryptoHandler.ValidateJweCreditCardAsync(jwe);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("refresh-token/{refreshToken}")]
        public async Task<IActionResult> GetAccessTokenByRefreshTokenAsync([FromRoute] Guid refreshToken)
        {
            var token = await _cryptoHandler.GetRefreshToken(refreshToken);

            // Refresh Token inválido / não encontrado
            if (token == null)
            {
                AddError("Invalid refresh token");
                return CustomResponse();
            }

            // Refresh Token expirado
            if (token.ExpirationDate <= DateTime.UtcNow)
            {
                AddError("Expired refresh token");
                return CustomResponse();
            }

            // Refresh Token válido para gerar novo access token (jwt)
            var accessToken = await GenerateJwt(token.UserName);
            var newRefreshToken = await _cryptoHandler.CreateRefreshTokenAsync(token.UserName);

            var response = new
            {
                access_token = accessToken,
                refreshToken = newRefreshToken.Token
            };

            return CustomResponse(response);
        }

        [HttpGet]
        [Route("refresh-token-jwt/{refreshToken}")]
        public async Task<IActionResult> GetAccessTokenByRefreshTokenAsync([FromRoute] string refreshToken)
        {
            var userName = await _cryptoHandler.GetSubjectFromJwtRefreshToken(refreshToken);

            // Refresh Token inválido / expirado
            if (string.IsNullOrEmpty(userName))
            {
                AddError("Expired refresh token");
                return CustomResponse();
            }

            // Refresh Token válido para gerar novo access token (jwt)
            var accessToken = await GenerateJwt(userName);
            var newRefreshToken = await _cryptoHandler.CreateJwtRefreshTokenAsync(_options.Value.Issuer, await GetJwtClaims(userName), DateTime.UtcNow.AddMinutes(480)); // 8 horas

            var response = new
            {
                access_token = accessToken,
                refreshToken = newRefreshToken
            };

            return CustomResponse(response);
        }


        private async Task<string> GenerateJwt(string userEmail)
        {
            // Gera um token passando um emissor, as claims
            // e o tempo de expiração do token em minutos
            var jws = await _cryptoHandler.CreateJwtTokenAsync(_options.Value.Issuer, await GetJwtClaims(userEmail), DateTime.UtcNow.AddMinutes(_options.Value.ExpirationMinutes));

            // Retornando o JWS
            return jws;
        }

        private async Task<ClaimsIdentity> GetJwtClaims(string userEmail)
        {
            // É possível complementar a criação do JWT com claims
            // tornando o JWT mais completo. 

            // Podem ser claims do Identity ou claims da conveção JWT, ou os dois juntos

            //⚠️ É sempre recomendado incluir as claims do JWT 
            // que identificam o usuário como: nome, e-mail, etc.

            // Aqui estamos recuperando o usuário do Identity, suas claims e roles
            var user = await _userManager.FindByNameAsync(userEmail);
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            // Aqui estamos adicionando as claims do JWT junto das claims do Identity
            // Sub: Identificador do usuário do JWT
            // Email: Email do usuário do JWT
            // Jti: Identificador único do JWT
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            // Nbf: (not before) NÃO pode ser aceito antes desse horário
            // Iat: (issued at) horário que foi emitido o JWT
            // O método ToUnixEpochDate() pode ser encontrado aqui.
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer32));

            // Adiciona as roles como claims do Identity
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            // Transfere todas as claims para o novo objeto
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }
}
