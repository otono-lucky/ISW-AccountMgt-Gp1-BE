using System.ComponentModel.DataAnnotations;
using static AccountMgt.Domain.Constants;

namespace AccountMgt.Api.Models
{
    public class UserModel
    {
        [RegularExpression(ValidationRegex.General)]
        [MaxLength(50)]
        public Guid Id { get; set; }
        [RegularExpression(ValidationRegex.General)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [RegularExpression(ValidationRegex.General)]
        [MaxLength(50)]
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        [RegularExpression(ValidationRegex.General)]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; }
    }
}
