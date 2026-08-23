 
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Threading.Tasks; 
namespace TASHTIP.EF.UsersRolePolicy
{
    public static class Permissions
    {
        public readonly static Controller Controller; 
        public static List<string> GeneratePermissionsList(string module )
        {
            var permissions = new List<string>();
            //buttons
            if (module == Controller.Admin.ToString() )
            {
                permissions.Add($"Permissions.{module}");
            }
            else if (module == Controller.User.ToString() )
            {
                permissions.Add($"Permissions.{module}");
            }
            
            return permissions;
           
        }


        public static List<string> GenerateAllPermissions()
        {
            var AllPermissions = new List<string>();

            var moduls = Enum.GetValues(typeof(Controller));
             
            foreach (var modul in moduls)
            {
                AllPermissions.AddRange(GeneratePermissionsList(  modul.ToString() ));
            }

            return AllPermissions;
        }

    }
}
