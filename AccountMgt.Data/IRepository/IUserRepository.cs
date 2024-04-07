using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Data.IRepository
{
    public interface IUserRepository
    {
        Task<string> RegisterUser(RegisterDto request);
        Task<string> Login(LoginDto loginDto);
    }
}
