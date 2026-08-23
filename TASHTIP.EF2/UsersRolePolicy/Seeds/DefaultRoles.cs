using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TASHTIP.EF.UsersRolePolicy.Seeds
{
    public static class DefaultRoles
    {
        /// <summary>
        /// Idempotent: creates the Admin/Customer roles if they don't already exist yet,
        /// and makes sure each carries the "Permission" claim the rest of the app checks
        /// (see Permissions.cs / _Header.cshtml's "Permissions.Admin" check).
        /// </summary>
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync(Roles.Admin.ToString());
            if (adminRole == null)
            {
                adminRole = new IdentityRole(Roles.Admin.ToString());
                await roleManager.CreateAsync(adminRole);
            }
            await roleManager.AddPermissionClaims(adminRole, "Admin");

            var customerRole = await roleManager.FindByNameAsync(Roles.Customer.ToString());
            if (customerRole == null)
            {
                customerRole = new IdentityRole(Roles.Customer.ToString());
                await roleManager.CreateAsync(customerRole);
            }
            await roleManager.AddPermissionClaims(customerRole, "User");
        }

        private static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            var existingClaims = await roleManager.GetClaimsAsync(role);
            var permissions = Permissions.GeneratePermissionsList(module);

            foreach (var permission in permissions)
            {
                if (!existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission, ClaimValueTypes.String, "LOCAL AUTHORITY"));
                }
            }
        }
    }
}
