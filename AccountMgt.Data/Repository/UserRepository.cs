using AccountMgt.Auth;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using AccountMgt.Utility.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace AccountMgt.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenGenerator tokenGenerator, IEmailService emailService, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
            _config = config;
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

        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Username);
            if (user == null)
            {
                return "User not found";
            }

            var attemptsLeft = await _userManager.GetAccessFailedCountAsync(user);
            if (attemptsLeft >= 2)
            {
                // Lock the user out for 2 hours
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddHours(2));

                var email = new EmailDto
                {
                    To = loginDto.Username,
                    Subject = "Suspicious Access",
                    UserName = loginDto.Username,
                    
                };
                // Notify account owner via email
                await _emailService.SendLockoutNotificationAsync(email);

                return "Your account has been locked out for 2 hours due to multiple failed login attempts.";
            }
            var Password = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!Password)
            {
                // Increment failed attempts
                await _userManager.AccessFailedAsync(user);

                attemptsLeft++;
                var remainingAttempts = 2 - attemptsLeft;
                return $"Invalid credentials. You have {remainingAttempts} attempts remaining.";
            }
            // Reset failed attempts upon successful login
            await _userManager.ResetAccessFailedCountAsync(user);

            var roles = await _userManager.GetRolesAsync(user);
            var UserRoles = roles.ToArray();

            //var token = _tokenGenerator.GenerateTokenAsync(user);

            var token = _tokenGenerator.GenerateToken(loginDto.Username, user.Id, loginDto.Password, _config, UserRoles).ToString();
            return token;

        }

        private string GenerateOtp()
        {
            var random = new Random();
            var otp = random.Next(1000, 9999).ToString();
            return otp;
        }
    }
}
