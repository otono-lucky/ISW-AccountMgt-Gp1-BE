using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Model.Entities
{
    public class Bank : BaseEntity
    {
        public string BankName { get; set; }
        public decimal BankBalance { get; set; }
        public Guid ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}
