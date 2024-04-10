using AccountMgt.Core.IServices;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Core.Services
{
    public class ProfileServices : IProfileSservices
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileServices(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;  
        }

        public async Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile)
        {
            var newUserProfile = await _profileRepository.CreateProfile(profile);
            if (newUserProfile == null)
            {
                return null;
            }
           
            return newUserProfile;
        }

        public async Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance)
        {
            var userProfileBalance = await _profileRepository.UpdateProfileBalance(profileBalance);
            if (userProfileBalance == null)
            {
                return null;
            }

            return userProfileBalance;
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
