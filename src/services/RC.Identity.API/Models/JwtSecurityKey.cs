namespace RC.Identity.API.Models
{
    public class JwtSecurityKey
    {
        public long Id { get; set; }
        public Guid KeyId { get; set; }
        public string? PublicParameters { get; set; }
        public string? PrivateKey { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
