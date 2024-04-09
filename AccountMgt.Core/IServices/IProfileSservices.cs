using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Core.IServices
{
    public interface IProfileSservices
    {
        Task<IList<GetAllProfileDto>> GetAllProfileByUserId(Guid UserId);
    }
}
