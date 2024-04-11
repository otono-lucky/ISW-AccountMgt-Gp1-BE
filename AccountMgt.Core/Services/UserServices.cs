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

        public async Task<string> ConfirmEmail(string username, string otp)
        {
            var result = await _userRepository.ConfirmEmail(username, otp);
            if (result != null)
            {
                return result;
            }
            return "Could not confirm email";
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
        public async Task<string> ForgotPassword(string email)
        {
            var result = await _userRepository.ForgotPassword(email);
            if (result != null) 
            {
                return result;
            }
            return "email not found";
        }

        public async Task<string> ResetPassword(string email, string token, string newPassword)
        {
            var result = await _userRepository.ResetPassword(email, token, newPassword);
            if (result != null)
            {
                return result;
            }
            return "Password cannot be reset at this time";
        }
    }
}
