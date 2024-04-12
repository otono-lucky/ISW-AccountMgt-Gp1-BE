﻿using AccountMgt.Model.DTO;
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
        Task<IList<GetAllProfileDto>> GetAllProfileByUserId(Guid UserId);
        Task<Profile> GetProfileById(Guid Id);
        Task DeleteProfileById(Guid Id);
        Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile);
        Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto profileBalance);
    }
}
