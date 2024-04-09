using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
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
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
