﻿using AccountMgt.Model.DTO;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Core.IServices
{
    public interface IProfileSservices
    {
        Task<IList<GetAllProfileDto>> GetAllProfileByUserId(Guid UserId);
        Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile);
        Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance);
    }
}
