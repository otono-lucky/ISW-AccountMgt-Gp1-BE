using AccountMgt.Model.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Model.Entities
{
    public class Profile : BaseEntity
    {
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Purpose { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
        public string? BankNumber { get; set; }
        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        
        
    }
}
