using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.Entities.Production
{
    public  class ServicesType
    {
        [Key]
        public int servicesID { get; set; }
        [MaxLength (50)]
        public string Services { get; set; }
    }
}
