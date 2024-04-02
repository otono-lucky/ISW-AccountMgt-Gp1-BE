using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccountMgt.Domain.Entities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EmailAddress { get; set; }
        public string Role { get; set; } 
        public DateTime CreatedOn { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string AccountStatus { get; set; }

    }
}
