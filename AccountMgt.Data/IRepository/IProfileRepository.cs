using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Data.IRepository
{
    public interface IProfileRepository
    {
        Task DeleteProfileById(string id);
        Task<IList<GetAllProfileDto>> GellAllProfileByUserId(string userId);
        Task<Profile> GetProfilebyId(string Id);
        Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile);
        Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance);
    }
}
