﻿using System.Text;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using RC.Identity.API.Data.Repositories;
using RC.Identity.API.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.JsonWebTokens;

namespace RC.Identity.API.CryptoHandlers
{
    public class RsaCryptoHandler : ICryptoHandler
    {
        private readonly IRepository _repository;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;

        public RsaCryptoHandler(IRepository securityKeyRepository, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _repository = securityKeyRepository;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        public async Task<string> CreateJwtTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires)
        {
            // Recupera chave do cache em memória
            var privateKey = _memoryCache.Get<string>("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                var key = await _repository.GetCurrentPrivateKeyAsync();

                privateKey = key?.PrivateKey;

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();

                    key = await _repository.GetCurrentPrivateKeyAsync();

                    privateKey = key?.PrivateKey ?? throw new NullReferenceException("Failed to provide a private key");
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
                TokenType = "at+jwt",
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                }
            });

            // Assina o token com o SigningCredentials
            // e retorna um JWS
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> CreateJwtTokenFromDistributedCacheAsync(string issuer, ClaimsIdentity? subject, DateTime expires)
        {
            // Recupera chave do cache distribuído
            var privateKey = _distributedCache.GetString("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                var key = await _repository.GetCurrentPrivateKeyAsync();

                privateKey = key?.PrivateKey;

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();

                    key = await _repository.GetCurrentPrivateKeyAsync();

                    privateKey = key?.PrivateKey ?? throw new NullReferenceException("Failed to provide a private key");
                }

                // Salva no cache por 15 minutos
                await _distributedCache.SetAsync("privateKey", Encoding.UTF8.GetBytes(privateKey), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                });
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
                TokenType = "at+jwt",
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                }
            });

            // Assina o token com o SigningCredentials
            // e retorna um JWS
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ValidateJweCreditCardAsync(string jwe)
        {
            // Recupera chave do cache em memória
            var privateKey = _memoryCache.Get<string>("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                var key = await _repository.GetCurrentPrivateKeyAsync();

                privateKey = key?.PrivateKey;

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();

                    key = await _repository.GetCurrentPrivateKeyAsync();

                    privateKey = key?.PrivateKey ?? throw new NullReferenceException("Failed to provide a private key");
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

            // Cria uma nova credencial de encriptação usando os algoritmos recomendados
            var decryptKey = new EncryptingCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaOAEP, SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenHandler = new JsonWebTokenHandler();

            // Valida e já descriptografa o JWE recebido
            var result = tokenHandler.ValidateToken(jwe, new TokenValidationParameters()
            {
                ValidIssuer = "https://localhost:7241",
                ValidAudience = "cartao-credito",
                RequireSignedTokens = false,
                TokenDecryptionKey = decryptKey.Key
            });

            var isValid = result.IsValid;

            // Se for válido, acessa as claims
            // onde uma delas é o cartão de crédito
            if (isValid)
            {
                var claims = result.Claims;
            }

            // Retorna se é válido
            return isValid;
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
            var now = DateTime.UtcNow;

            await _repository.AddSecurityKeyAsync(new JwtSecurityKey()
            {
                KeyId = keyId,
                CreationDate = now,

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

            await _repository.AddPrivateKeyAsync(new JwtPrivateKey()
            {
                PrivateKey = privateKeyBuffer.ToString(),
                CreationDate = now
            });
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(string email)
        {
            var refreshToken = new RefreshToken
            {
                UserName = email,
                ExpirationDate = DateTime.UtcNow.AddHours(8)
            };

            await _repository.AddRefreshTokenAsync(refreshToken);

            return refreshToken;
        }

        public async Task<string> CreateJwtRefreshTokenAsync(string issuer, ClaimsIdentity? subject, DateTime expires)
        {
            // Recupera chave do cache em memória
            var privateKey = _memoryCache.Get<string>("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                var key = await _repository.GetCurrentPrivateKeyAsync();

                privateKey = key?.PrivateKey;

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();

                    key = await _repository.GetCurrentPrivateKeyAsync();

                    privateKey = key?.PrivateKey ?? throw new NullReferenceException("Failed to provide a private key");
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
                TokenType = "rt+jwt",
                Audience = "refresh-token",
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
                {
                    CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
                }
            });

            // Assina o token com o SigningCredentials
            // e retorna um JWS que atuará como Refresh Token
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken?> GetRefreshToken(Guid refreshToken)
        {
            return await _repository.GetRefreshTokenAsync(refreshToken);
        }

        public async Task<string?> GetSubjectFromJwtRefreshToken(string refreshToken)
        {
            // Recupera chave do cache em memória
            var privateKey = _memoryCache.Get<string>("privateKey");

            // Se não existir, recupera chave privada mais recente da base
            if (privateKey == null)
            {
                var key = await _repository.GetCurrentPrivateKeyAsync();

                privateKey = key?.PrivateKey;

                // Se não existir, cria um par de chaves novo
                if (privateKey == null)
                {
                    await CreateKeysAsync();

                    key = await _repository.GetCurrentPrivateKeyAsync();

                    privateKey = key?.PrivateKey ?? throw new NullReferenceException("Failed to provide a private key");
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

            var signKey = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = false }
            };

            var tokenHandler = new JsonWebTokenHandler();

            // Valida a assinatura do refresh token
            var result = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters()
            {
                ValidIssuer = "https://localhost:7241",
                ValidAudience = "refresh-token",
                RequireSignedTokens = false,
                IssuerSigningKey = signKey.Key
            });

            if (!result.IsValid)
            {
                tokenHandler = new JsonWebTokenHandler();

                // Valida a assinatura do refresh token
                result = tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters()
                {
                    ValidIssuer = "https://localhost:7241",
                    ValidAudience = "refresh-token",
                    RequireSignedTokens = false,
                    IssuerSigningKey = signKey.Key
                });
            }

            if (result.IsValid)
            {
                return result.Claims["email"].ToString();
            };

            return null;
        }
    }
}
