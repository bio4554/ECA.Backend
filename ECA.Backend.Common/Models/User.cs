namespace ECA.Backend.Common.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? CreatedDate { get; set; }
        public string? Email { get; set; }
    }
}