using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Core.IServices
{
    public interface IProfileSservices
    {
        Task<IList<GetAllProfileDto>> GetAllProfileByUserId(string UserId);
        Task<Profile> GetProfileById(string Id);
        Task DeleteProfileById(string Id);
        Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile);
        Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance);
    }
}
