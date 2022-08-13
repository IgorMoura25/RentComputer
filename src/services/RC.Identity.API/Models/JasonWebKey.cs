using System.Text.Json.Serialization;

namespace RC.Identity.API.Models
{
    public class JasonWebKey
    {
        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc7517#section-4.1
        /// The "kty" (key type) parameter identifies the cryptographic algorithm family used with the key, such as "RSA" or "EC"
        /// </summary>
        [JsonPropertyName("kty")]
        public string? KeyType { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc7517#section-4.5
        /// The "kid" (key ID) parameter is used to match a specific key
        /// </summary>
        [JsonPropertyName("kid")]
        public string? KeyId { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc7517#section-4.4
        /// The "alg" (algorithm) parameter identifies the algorithm intended for use with the key
        /// </summary>
        [JsonPropertyName("alg")]
        public string? Algorithm { get; set; }

        /// <summary>
        /// https://www.rfc-editor.org/rfc/rfc7517#section-4.2
        /// The "use" (public key use) parameter identifies the intended use of the public key
        /// </summary>
        [JsonPropertyName("use")]
        public string? Use { get; set; }

        [JsonPropertyName("n")]
        public string? Modulus { get; set; }

        [JsonPropertyName("e")]
        public string? Exponent { get; set; }
    }
}
