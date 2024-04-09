using AccountMgt.Model.DTO;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Data.IRepository
{
    public interface IProfileRepository
    {
        Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile);
        Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance);
    }
}
