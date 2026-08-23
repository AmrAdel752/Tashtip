using Microsoft.AspNetCore.Authorization;

namespace Web.Models.Filter
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; private set; }

        public PermissionRequirement(string Permission)
        {
            this.Permission = Permission;   
        }
    }
}
