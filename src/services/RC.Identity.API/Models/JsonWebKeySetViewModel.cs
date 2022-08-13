using System.Text.Json.Serialization;

namespace RC.Identity.API.Models
{
    public class JsonWebKeySetViewModel
    {
        [JsonPropertyName("keys")]
        public List<JasonWebKey>? Keys { get; set; }
    }
}
