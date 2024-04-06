using AccountMgt.Auth;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using AccountMgt.Utility.Email;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        //private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailService _emailService;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, /*ITokenGenerator tokenGenerator,*/ IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
          //  _tokenGenerator = tokenGenerator;
            _emailService = emailService;
        }

        public async Task<string> RegisterUser(RegisterDto request)
        {
            var appUser = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email,
                City = request.City,
                State = request.State,
                Country = request.Country,
            };
            var email = new EmailDto
            {
                To = request.Email,
                Subject = "Registration Successful",
                UserName = request.Email,
                Otp = GenerateOtp()
            };
            var createUser = await _userManager.CreateAsync(appUser, request.Password);
            if(createUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                if(roleResult.Succeeded)
                {
                    await _emailService.SendEmailRegistration(email);
                    return "User registered successful";
                }
                
            }
            return "Something went wrong, user could not be registered";
        }

        private string GenerateOtp()
        {
            var random = new Random();
            var otp = random.Next(1000, 9999).ToString();
            return otp;
        }
    }
}
