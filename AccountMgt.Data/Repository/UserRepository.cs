using AccountMgt.Auth;
using AccountMgt.Data.IRepository;
using AccountMgt.Model.DTO;
using AccountMgt.Model.Entities;
using AccountMgt.Utility.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly AppDbContext _context;

        public UserRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenGenerator tokenGenerator, IEmailService emailService, IConfiguration config, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
            _config = config;
            _context = context;
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
                Otp = GenerateOtp()
            };
            var email = new EmailDto
            {
                To = request.Email,
                Subject = "Registration Successful",
                UserName = request.Email,
                Otp = appUser.Otp
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

        public async Task<string> ConfirmEmail(string username, string otp)
        {
            // Retrieve the user by email
            var user = await _context.appUsers.FirstOrDefaultAsync(o => o.UserName == username);

            if (user == null)
            {
                return "User not found";
            }

            // Check if the OTP matches
            if (user.Otp != otp)
            {
                return "Invalid OTP";
            }

            // Update user email confirmation status
            user.EmailConfirmed = true;

            // Update user's OTP to null (assuming OTP should be used only once)
            user.Otp = null;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return "Email confirmed successfully";
            }
            else
            {
                return "Failed to confirm email";
            }
        }


        public async Task<string> Login(LoginDto loginDto)
        {
            var user = await _context.appUsers.FirstOrDefaultAsync(e => e.UserName == loginDto.Username);
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


        public async Task<string> ForgotPassword(string email)
        {
            // Find the user by email
            var user = await _context.appUsers.FirstOrDefaultAsync(x => x.UserName == email);

            if (user == null)
            {
                // User not found
                return "User not found";
            }

            // Generate a new password reset token
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Construct the password reset link with token
            var resetLink = $"https://yourwebsite.com/resetpassword?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

            // You can send an email with the password reset link to the user
            var emailContent = new EmailDto
            {
                To = email,
                Subject = "Password Reset",
                Body = $"Please click the following link to reset your password: {resetLink}"
            };

            await _emailService.SendForgotPasswordEmailAsync(emailContent);

            return "Password reset link sent to your email";
        }


        private string GenerateOtp()
        {
            var random = new Random();
            var otp = random.Next(1000, 9999).ToString();
            return otp;
        }
    }
}
