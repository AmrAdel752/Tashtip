using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.Entities.Production
{
    public  class City
    {
        [Key]
        public int CityID { get; set; }
        [MaxLength (100)]
        public string  CityName { get; set; }
    }
}
