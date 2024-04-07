﻿using AccountMgt.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Auth
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        public TokenGenerator(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public object GenerateToken(string username, string id, string email, IConfiguration config, string[] roles)
        {
            //Create and Setup claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, id),
            };

            //create and setup roles
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //create security token descriptor
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:JWTSigningkey").Value)),
                SecurityAlgorithms.HmacSha256Signature)
            };

            //create a token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenCreated = tokenHandler.CreateToken(securityTokenDescriptor);

            return new
            {
                token = tokenHandler.WriteToken(tokenCreated).ToString(),
                Id = id
            };
        }
    }
}
