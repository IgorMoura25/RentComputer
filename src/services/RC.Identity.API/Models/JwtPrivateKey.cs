namespace RC.Identity.API.Models
{
    public class JwtPrivateKey
    {
        public long Id { get; set; }
        public string? PrivateKey { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
