using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using RC.Identity.API.Data.Repositories;
using RC.Identity.API.Models;

namespace RC.Identity.API.CryptoHandlers
{
    public class RsaCryptoHandler : ICryptoHandler
    {
        private readonly ISecurityKeyRepository _securityKeyRepository;
        private readonly IMemoryCache _memoryCache;

        public RsaCryptoHandler(ISecurityKeyRepository securityKeyRepository, IMemoryCache memoryCache)
        {
            _securityKeyRepository = securityKeyRepository;
            _memoryCache = memoryCache;
        }

        public async Task<string> CreateJwtTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires)
        {
            // Recupera chave do cache em memória
            var privateKey = _memoryCache.Get<string>("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                privateKey = await _securityKeyRepository.GetCurrentPrivateKeyAsync();

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();
                    privateKey = await _securityKeyRepository.GetCurrentPrivateKeyAsync() ?? throw new NullReferenceException("Failed to provide a private key");
                }

                // Salva no cache por 15 minutos
                _memoryCache.Set("privateKey", privateKey, TimeSpan.FromMinutes(15));
            }

            // Converte de Base64Url para bytes
            var privateKeyBytes = Convert.FromBase64String(privateKey);


            // Instancia um novo RSA
            using var rsa = RSA.Create();

            // Importa a chave privada
            rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

            var tokenHandler = new JwtSecurityTokenHandler();

            // Cria o token com as credenciais de assinatura
            // RSA como criptografia de chave e SHA256 como função de hash
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                // Exemplo 1: https://auth.rentcomputer.api
                // Exemplo 2: http://localhost:7321
                Issuer = issuer,
                Subject = subject,
                Expires = expires,
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                }
            });

            // Assina o token com o SigningCredentials
            // e retorna um JWS
            return tokenHandler.WriteToken(token);
        }

        public async Task CreateKeysAsync()
        {
            // Cria uma chave RSA de 2048 bits (mínimo requerido)
            var key = RSA.Create(2048);

            // Exporta a chave privada, convertendo seus bytes para Base64Url
            var privateKeyBuffer = new StringBuilder();
            privateKeyBuffer.AppendLine(Convert.ToBase64String(key.ExportRSAPrivateKey(), Base64FormattingOptions.None));

            // Exporta os parâmetros públicos da chave
            // para criação do JWK posteriormente
            var parameters = key.ExportParameters(false);

            // Gera um identificador único para a chave
            var keyId = Guid.NewGuid();

            // Salva na base
            await _securityKeyRepository.AddAsync(new JwtSecurityKey()
            {
                KeyId = keyId,
                PrivateKey = privateKeyBuffer.ToString(),
                CreationDate = DateTime.UtcNow,

                // Sendo o campo "PublicParameters" um JWK
                // que será usado para validação dos tokens emitidos
                PublicParameters = JsonSerializer.Serialize(new JasonWebKey()
                {
                    KeyType = "RSA",
                    KeyId = keyId.ToString(),
                    Algorithm = "RS256",
                    Use = "sig",
                    Modulus = Base64UrlEncoder.Encode(parameters.Modulus),
                    Exponent = Base64UrlEncoder.Encode(parameters.Exponent)
                }),
            });
        }
    }
}
