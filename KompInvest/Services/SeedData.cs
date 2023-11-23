using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace KompInvest.Services
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            // Add specific roles
            string[] roleNames = { "Admin", "User", "Manager" };
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    // Create the roles and seed them to the database
                    var createRoleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!createRoleResult.Succeeded)
                    {
                        // Handle role creation failure
                        throw new InvalidOperationException($"Failed to create role {roleName}");
                    }
                }
            }

            // Create a super user who could maintain the web app
            var poweruser = new IdentityUser
            {
                UserName = "adminuser@example.com",
                Email = "adminuser@example.com",
            };

            string userPWD = "Omolokonyo1@";
            var user = await userManager.FindByEmailAsync("adminuser@example.com");

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    // Assign the new user the "Admin" role 
                    var addToRoleResult = await userManager.AddToRoleAsync(poweruser, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        // Handle role assignment failure
                        throw new InvalidOperationException("Failed to assign Admin role to super user");
                    }
                }
                else
                {
                    // Handle user creation failure
                    throw new InvalidOperationException("Failed to create super user");
                }
            }
        }
    }
}

