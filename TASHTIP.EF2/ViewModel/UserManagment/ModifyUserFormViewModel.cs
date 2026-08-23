using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.ViewModel.UserManagment
{
    public class ModifyUserFormViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name ="User Name")]
        [StringLength(100)]
        public string UserName { get; set; }


        [Required, Display(Name = "Email")]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }


        /*
        [Required]
        [Display(Name = "New Password")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }


        [Required]
        [Display(Name = "Confirm New Password")]
        [StringLength(100)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "confirm new password does not match")]
        public string ConfirmNewPassword { get; set; }
        */
         

    }
}
