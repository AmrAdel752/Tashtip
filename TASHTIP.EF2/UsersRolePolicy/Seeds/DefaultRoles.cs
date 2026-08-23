 
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TASHTIP.EF.UsersRolePolicy.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRoleAsync(RoleManager<IdentityRole> roleManager)
        {
            //if (roleManager.Roles.Any())
            //{
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Account.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Production.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.CR.ToString()));
                await roleManager.CreateAsync(new IdentityRole(Roles.Currancy.ToString()));
            //} 
        }
    }
}
