using AccountMgt.Core.IServices;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Core.Services
{
    public class ProfileServices : IProfileSservices
    {
        private readonly IProfileRepository _profileRepository;
        public ProfileServices(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<IList<GetAllProfileDto>> GetAllProfileByUserId(Guid UserId)
        {
            IList<GetAllProfileDto> result = await _profileRepository.GellAllProfileByUserId(UserId);
            if (result != null)
            {
                return result;
            }
            return new List<GetAllProfileDto>();
        }
    }
}
