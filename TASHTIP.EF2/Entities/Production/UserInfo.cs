using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Net; 
using System.Text;
using System.Threading.Tasks; 

namespace TASHTIP.EF.Entities.Production
{
    public class UserInfo
    {
        private readonly static IHttpContextAccessor CurrentNameLogin = new HttpContextAccessor();

        [MaxLength(150)]
        public string? UserAction { get; set; } = CurrentNameLogin.HttpContext.User.Identity.Name;
        [MaxLength(50)]
        public string? DateTimeAction { get; set; } = DateTime.Now.ToString();
        [MaxLength(100)]
        public string? DeviceNameAction { get; set; } = Dns.GetHostName();
    }
}
