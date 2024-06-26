﻿using AccountMgt.Core.IServices;
using AccountMgt.Model.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountMgt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost("add-user")]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto request)
        {
            return Ok(await _userServices.CreateUserService(request));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            return Ok(await _userServices.Login(loginDto));
        }

        [HttpPatch("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string username, string otp)
        {
            return Ok(await _userServices.ConfirmEmail(username, otp));
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return Ok(await _userServices.ForgotPassword(email));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            return Ok(await _userServices.ResetPassword(email, token, newPassword));
        }

        [HttpPatch("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            return Ok(await _userServices.ChangePassword(model));
        }

        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return Ok(_userServices.GetUserById(id));
        }
    }
}
