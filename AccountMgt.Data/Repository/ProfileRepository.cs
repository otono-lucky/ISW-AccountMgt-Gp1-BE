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
        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }

        
    }
}
