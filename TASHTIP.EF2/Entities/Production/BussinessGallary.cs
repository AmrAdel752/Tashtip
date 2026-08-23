using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TASHTIP.EF.Entities.Production
{
    public  class BussinessGallary  
    {
        [Key ]
        public int ID { get; set; }

        public string  ServicesName { get; set; }  
        public string  City { get; set; } 
        public string  Engineer { get; set; }
        public decimal Price { get; set; }

        public string  Vendor { get; set; }

        [MaxLength(20)]
        public string BussinessDate { get; set; } 
        public string  DetailsUnit { get; set; }
        public string InteriorDesign { get; set; }
        public string FinishingQuality { get; set; }

        public string  LinkVideo { get; set; }
        public string  ProfileImage { get; set; }

        public string Filter { get; set; }
    }
}
