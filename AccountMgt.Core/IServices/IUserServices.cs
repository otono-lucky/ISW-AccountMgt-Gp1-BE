﻿using AccountMgt.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Core.IServices
{
    public interface IUserServices
    {
        Task<string> CreateUserService(RegisterDto request);
        Task<string> ConfirmEmail(string username, string otp);
        Task<string> Login(LoginDto loginDto);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(string email, string token, string newPassword);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
    }
}
