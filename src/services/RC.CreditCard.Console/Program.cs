using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RC.CreditCard.Console;

var publicKey = await JwksRetriever.GetPublicKey();

// Instancia as credencias de criptografia
var encryptCredentials = new EncryptingCredentials(publicKey, SecurityAlgorithms.RsaOAEP, SecurityAlgorithms.Aes128CbcHmacSha256);

// Define o JWT passando as credenciais
// Neste exemplo, estamos criptografando dados sensíveis
// para transferência entre apps, como o cartão de crédito
var tokenJwt = new SecurityTokenDescriptor()
{
    Issuer = "https://localhost:7241", // Destinatário válido
    Audience = "cartao-credito", // Opcional, se quiser adicionar mais uma camada de validação no lado do destinatário
    Subject = new ClaimsIdentity(new[] { new Claim("cc", "4000-0000-0000-0002") }),
    EncryptingCredentials = encryptCredentials
};

// Instancia o handler do JWT
var tokenHandler = new JwtSecurityTokenHandler();

// Cria o token
var token = tokenHandler.CreateToken(tokenJwt);

// Criptografa o JWE
var jwe = tokenHandler.WriteToken(token);

Console.WriteLine(jwe);
