using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.ViewModel.UserManagment
{
    public class PermissionFormViewModel
    {
        
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public List<RoleViewModel> RoleClaims { get; set; }
    }
}
