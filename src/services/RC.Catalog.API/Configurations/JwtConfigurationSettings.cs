namespace RC.Catalog.API.Configurations
{
    public class JwtConfigurationSettings
    {
        public string? RetrievalUrl { get; set; }
        public bool RequiredHttps { get; set; } = true;
    }
}
