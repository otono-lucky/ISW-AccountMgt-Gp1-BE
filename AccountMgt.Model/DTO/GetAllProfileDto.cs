using AccountMgt.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Model.DTO
{
    public class GetAllProfileDto
    {
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public string Purpose { get; set; }
        public string BankName { get; set; }
        public string BankNumber { get; set; }
    }
}
