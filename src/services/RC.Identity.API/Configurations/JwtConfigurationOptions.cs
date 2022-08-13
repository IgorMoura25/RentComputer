namespace RC.Identity.API.Configurations
{
    public class JwtConfigurationOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; } = 1;
    }
}
