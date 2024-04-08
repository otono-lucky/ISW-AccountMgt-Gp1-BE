using AccountMgt.Data.IRepository;
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

        ///private readonly ProfileManager<AppProfile> _profileManager;

       /* public ProfileRepository(ProfileManager<AppProfile> profileManager, AppDbContext context)
        {
            _profileManager = profileManager;
            _context = context;
        }*/

        /*public async Task<string> GetAllProfile(GetAllProfileDto request)
        {
            var profiles = await _profileManager.GetAllProfileAsync(userId);
            if (profiles == null)
            throw new EntityNotFoundException(ids);
            return profiles;
        }*/
    }
}
