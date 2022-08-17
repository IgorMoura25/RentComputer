namespace RC.Identity.API.Models
{
    public class RefreshToken
    {
        public RefreshToken()
        {
            Token = Guid.NewGuid();
        }

        public long Id { get; set; }
        public Guid Token { get; set; }
        public string? UserName { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
