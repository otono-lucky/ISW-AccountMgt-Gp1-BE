﻿using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
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
        Task<string> ConfirmEmail(string username, string otp);
        Task<string> Login(LoginDto loginDto);
        Task<string> ForgotPassword(string email);
        Task<string> ResetPassword(string email, string token, string newPassword);
        Task<string> ChangePassword(ChangePasswordDTO model);
        Task<object> GetUserById(string id);
    }
}
