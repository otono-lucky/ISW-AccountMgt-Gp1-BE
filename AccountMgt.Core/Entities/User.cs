using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        public string? Image {  get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime LastLoginDate { get; set; }
    }
}
