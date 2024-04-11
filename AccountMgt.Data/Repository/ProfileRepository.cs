using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Data.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;
        private readonly IGenericRepository<Profile> _profiles;
        public ProfileRepository(AppDbContext context, IGenericRepository<Profile> profiles)
        {
            _context = context;
            _profiles = profiles;
        }
        public async Task<IList<GetAllProfileDto>> GellAllProfileByUserId(Guid userId)
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

        public async Task DeleteProfileById(Guid id)
        {
            var result = _profiles.BulkUpdate(query: x => x.Id == id, 
                setProperty: s => s.SetProperty(p => p.IsDeleted, true));
        }

        public async Task<Profile> GetProfilebyId(Guid Id)
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
