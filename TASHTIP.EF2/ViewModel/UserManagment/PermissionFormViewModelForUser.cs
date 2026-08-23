using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.ViewModel.UserManagment
{
    public class PermissionFormViewModelForUser
    {
       
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> UserClaims { get; set; }
    }
}
