using AccountMgt.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Model.Entities
{
    public class Profile : BaseEntity
    {
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public string Purpose { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        
        
    }
}
