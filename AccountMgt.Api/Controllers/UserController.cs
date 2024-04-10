using AccountMgt.Core.IServices;
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
    }
}
