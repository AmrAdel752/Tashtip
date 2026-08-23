 
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks; 

namespace TASHTIP.EF.UsersRolePolicy.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedUserAdminAsync(UserManager<IdentityUser> userManager )
        {
            //var defaultUser = new IdentityUser
            //{ 
            //    UserName = "Mohamed.Abdou",
            //    Email = "Mohamed.Abdou@gizapowerindustry.com",
            //    EmailConfirmed = true
            //}; 

            //var user = await userManager.FindByNameAsync(defaultUser.UserName); 

            //if (user == null)
            //{
            //    await userManager.CreateAsync(defaultUser ,"Medo@123");
            //    await userManager.AddToRoleAsync(defaultUser ,Roles.Admin.ToString());
            //}

            //var defaultUser2 = new IdentityUser
            //{ 
            //    UserName = "Ehab.Salem",
            //    Email = "Ehab.Salem@gizapowerindustry.com",
            //    EmailConfirmed = true
            //};

            //var user2 = await userManager.FindByNameAsync(defaultUser2.UserName);

            //if (user2 == null)
            //{
            //    await userManager.CreateAsync(defaultUser2, "Ehab@123");
            //    await userManager.AddToRoleAsync(defaultUser2, Roles.Admin.ToString());
            //}


        }
 


        //    public static async Task SeedUserAccountAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        //    {
        //        var defaultUser = new IdentityUser
        //        { 
        //            UserName = "Amr.Adel",
        //            Email = "Amr.Adel@gizapowerindustry.com",
        //            EmailConfirmed = true
        //        };

        //    var user = await userManager.FindByEmailAsync(defaultUser.Email);

        //    if (user == null)
        //    {
        //        await userManager.CreateAsync(defaultUser, "Amr@123");
        //        await userManager.AddToRolesAsync(defaultUser, new List<string> {Roles.Account.ToString(), Roles.Production.ToString() });
        //    }


        //    var defaultUser2 = new IdentityUser
        //    {
        //        UserName = "Abdelrahman.Ahmed",
        //        Email = "Abdelrahman.Ahmed@gizapowerindustry.com",
        //        EmailConfirmed = true
        //    };

        //    var user2 = await userManager.FindByEmailAsync(defaultUser2.Email);

        //    if (user2 == null)
        //    {
        //        await userManager.CreateAsync(defaultUser2, "Abdou@123");
        //        await userManager.AddToRolesAsync(defaultUser2, new List<string> { Roles.CR.ToString(), Roles.Currancy.ToString() });
        //    }



        //    //TODO: create seeds claims المطالبات and policy
        //    await roleManager.SeedsClaimsForUserAccount();


        //} 


        //private static async Task SeedsClaimsForUserAccount(this RoleManager<IdentityRole> roleManager)
        //{
        //    var AccountRole = await roleManager.FindByNameAsync(Roles.Account.ToString());
        //    await roleManager.AddPermissionClaims(AccountRole, "Account");
        //}

        //public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string Module)
        //{
        //    var AllClaims = await roleManager.GetClaimsAsync(role);
        //    var AllPermissions = Permissions.GeneratePermissionsList(Module);

        //    foreach (var permission in AllPermissions)
        //    {
        //        if (!AllClaims.Any(c=>c.Type == "Permission" && c.Value == permission))
        //        {
        //            await roleManager.AddClaimAsync(role,new Claim("Permission",permission)); 
        //        }
        //    }
        //}




    }
}
