namespace AccountMgt.Api.Models
{
    public class UserDetailModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsLockedOut { get; set; }
        public DateTimeOffset? LockOutDate { get; set; }
    }
}
