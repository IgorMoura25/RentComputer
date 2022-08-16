using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RC.Identity.API.Configurations;
using RC.Identity.API.CryptoHandlers;
using RC.Identity.API.Models;
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

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ICryptoHandler cryptoHandler, IOptions<JwtConfigurationOptions> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cryptoHandler = cryptoHandler;
            _options = options;
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
                await _signInManager.SignInAsync(user, isPersistent: false);

                return CustomResponse(await GenerateJwt(model.Email));
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
                return CustomResponse(await GenerateJwt(model.Email));
            }

            if (result.IsLockedOut)
            {
                AddError("User temporary blocked for invalid attempts");
                return CustomResponse();
            }

            AddError("The user or password is incorrect");

            return CustomResponse();
        }

        private async Task<string> GenerateJwt(string userEmail)
        {
            // Gera um token passando um emissor, as claims
            // e o tempo de expiração do token em minutos
            var jws = await _cryptoHandler.CreateJwtTokenFromDistributedCacheAsync(_options.Value.Issuer, await GetJwtClaims(userEmail), DateTime.UtcNow.AddMinutes(_options.Value.ExpirationMinutes));

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
