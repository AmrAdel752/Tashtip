using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Employee;

namespace TASHTIP.EF.UsersRolePolicy.Seeds
{
    public static class DefaultUsers
    {
        /// <summary>
        /// Seeds one Admin account so there is always a way into the back office on a
        /// fresh database. Credentials are documented in README.md — change the password
        /// immediately after first login (Identity's own /Identity/Account/Manage page).
        /// </summary>
        public static async Task SeedUserAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultAdmin = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@tashtip.com",
                EmailConfirmed = true,
                ChangePass = true
            };

            var existing = await userManager.FindByNameAsync(defaultAdmin.UserName);
            if (existing == null)
            {
                var result = await userManager.CreateAsync(defaultAdmin, "Tashtip@Admin1");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultAdmin, Roles.Admin.ToString());
                }
                existing = defaultAdmin;
            }

            if (!await userManager.IsInRoleAsync(existing, Roles.Admin.ToString()))
            {
                await userManager.AddToRoleAsync(existing, Roles.Admin.ToString());
            }
        }

        /// <summary>
        /// Seeds one Customer account with an easy-to-type password, purely so anyone
        /// trying the live demo has a ready-made login without registering. Credentials
        /// are documented in CREDENTIALS.md.
        /// </summary>
        public static async Task SeedDemoCustomerAsync(UserManager<ApplicationUser> userManager)
        {
            var demoUser = new ApplicationUser
            {
                UserName = "demo",
                Email = "demo@tashtip.com",
                EmailConfirmed = true,
                ChangePass = false
            };

            var existing = await userManager.FindByNameAsync(demoUser.UserName);
            if (existing == null)
            {
                var result = await userManager.CreateAsync(demoUser, "Demo@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(demoUser, Roles.Customer.ToString());
                }
                existing = demoUser;
            }

            if (!await userManager.IsInRoleAsync(existing, Roles.Customer.ToString()))
            {
                await userManager.AddToRoleAsync(existing, Roles.Customer.ToString());
            }
        }
    }
}
