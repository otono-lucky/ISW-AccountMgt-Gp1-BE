using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Data.IRepository
{
    public interface IProfileRepository
    {
        Task<IList<GetAllProfileDto>> GellAllProfileByUserId(Guid userId);
        Task<Profile> GetProfilebyId(Guid Id);
    }
}
