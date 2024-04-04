using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Model.Entities
{
    public class Transaction : BaseEntity
    {
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
    }
}
