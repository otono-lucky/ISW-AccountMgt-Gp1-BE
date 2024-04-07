using AccountMgt.Core.IServices;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Core.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;
        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> CreateUserService(RegisterDto request)
        {
           var newUser =  await _userRepository.RegisterUser(request);
            if (newUser != null)
            {
                return "User added successfully";
            }
            return "No user added";
        }

        public async Task<string> Login(LoginDto loginDto)
        {
            var token = await _userRepository.Login(loginDto);
            if (token != null)
            {
                return token;
            }
            return "Something went wrong";
        }
    }
}
