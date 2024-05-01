using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountMgt.Model.Entities;
using AccountMgt.Model.ResponseModels;

namespace AccountMgt.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        private readonly IGenericRepository<Profile> _profiles;
        public ProfileRepository(AppDbContext context, IGenericRepository<Profile> profiles)
        {
            _context = context; 
            _context = context;
            _profiles = profiles;
        }

        public async Task<CreateProfileResponseModel> CreateProfile(ProfileDto profile)
        {
            var userProfile = new Profile
            {
                AccountType = profile.AccountType,
                Balance = profile.Balance,
                UserId = profile.UserId,
                Purpose = profile.Purpose,
                BankName = profile.BankName,
              
            };

            var addedProfile = await _context.Set<Profile>().AddAsync(userProfile);

            await _context.SaveChangesAsync();

            var responseModel = new CreateProfileResponseModel
            {
                Success = true,
                Message = "Profile created successfully",
                Profile = new ProfileDto
                {
                    AccountType = userProfile.AccountType,
                    Balance = userProfile.Balance,
                    UserId = userProfile.UserId,
                    Purpose = userProfile.Purpose,
                    BankName= userProfile.BankName,
                }
            };

            return responseModel;

        }

        public async Task<UpdateProfileBalanceResponseModel> UpdateProfileBalance(UpdateProfileBalanceDto updateProfileBalanceDto)
        {
            var userProfile = await _context.Set<Profile>().FindAsync(updateProfileBalanceDto.UserId);

            if (userProfile == null)
            {
                return new UpdateProfileBalanceResponseModel
                {
                    Success = false,
                    Message = "User profile not found"
                };
            }

            userProfile.Balance = updateProfileBalanceDto.Balance;

            await _context.SaveChangesAsync();

            return new UpdateProfileBalanceResponseModel
            {
                Success = true,
                Message = "Profile balance updated successfully",
                Balance = userProfile.Balance
            };
        }

        public async Task<IList<GetAllProfileDto>> GellAllProfileByUserId(string userId)
        {
            var data = new List<GetAllProfileDto>();
            foreach(var profile in _context.Profiles.Where(e => e.UserId == userId))
            {
                data.Add(new GetAllProfileDto
                {
                    UserId = profile.UserId,
                    Balance = profile.Balance,
                    BankName = profile.BankName,
                    BankNumber = profile.BankNumber,
                    Purpose = profile.Purpose,
                });
            }
            return data;
        }

        public async Task DeleteProfileById(string id)
        {
            var result = _profiles.BulkUpdate(query: x => x.Id == id, 
                setProperty: s => s.SetProperty(p => p.IsDeleted, true));
        }

        public async Task<Profile> GetProfilebyId(string Id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(e => e.Id == Id);
            if (profile == null)
            {
                return null;
            }
            var data = new Profile
            {
                UserId = profile.UserId,
                Balance = profile.Balance,
                BankName = profile.BankName,
                BankNumber = profile.BankNumber,
                Purpose = profile.Purpose
            };
            return data;
        }
    }
}
