namespace AccountMgt.Api.Models
{
    public class AdminModel
    {
        public Guid Id { get; set; }
        public string EmailAddress { get; set; }
        public string Role { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; }
        public string AccountStatus { get; set; }
    }
}
