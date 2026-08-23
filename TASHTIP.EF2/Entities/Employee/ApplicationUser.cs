using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace TASHTIP.EF.Entities.Employee
{ 
    public class ApplicationUser : IdentityUser
    {  
        [Column (Order = 2)]
        public bool ChangePass { get; set; }
        [Column(Order = 3)]
        public string? UserGroup { get; set; }
        [Column(Order = 4)]
        public string? UserCategpry { get; set; } 
    }

    
}
