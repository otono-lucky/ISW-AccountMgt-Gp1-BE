using AccountMgt.Model.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountMgt.Data
{
    public class Seeder
    {
        public static async Task SeedData(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();
            await SeedRoles(roleManager);
            await SeedUsers(userManager, context);
        }

        private static async Task SeedUsers(UserManager<AppUser> userManager, AppDbContext context)
        {
            await CreateAndAssignUser(userManager, "Lucky", "Otono", "otono@gmail.com", "Ysb123@32", "2349064056077", "Edo", "Benin", "Nigeria", "User");
            await CreateAndAssignUser(userManager, "Lero", "Otono", "lero@gmail.com", "Ysb123@32", "+2349018015592", "PH", "Porthacort", "Nigeria", "Admin");
            
        }

        private static async Task CreateAndAssignUser(UserManager<AppUser> userManager, string firstName, string lastName, string email, string password, string phoneNumber, string city, string state, string country, string role)
        {
            if (!userManager.Users.Any(u => u.Email == email))
            {
                var user = new AppUser
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    ImageUrl = "openForCorrection",
                    UserName = email,
                    PhoneNumber = phoneNumber,
                    City = city,
                    State = state,
                    Country = country
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (roleManager.RoleExistsAsync("Admin").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);
            }
            if (roleManager.RoleExistsAsync("User").Result == false)
            {
                var role = new IdentityRole
                {
                    Name = "User"
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}
