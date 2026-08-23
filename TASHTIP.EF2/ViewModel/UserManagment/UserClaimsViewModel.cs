using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.ViewModel.UserManagment
{
    public class UserClaimsViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<string> UserClaims { get; set; }
    }
}
