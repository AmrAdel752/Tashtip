using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.Entities.Production
{
    public class FilterGallary
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string FilterName { get; set; }
        [MaxLength(50)]
        public string Section { get; set; }
    }
}
