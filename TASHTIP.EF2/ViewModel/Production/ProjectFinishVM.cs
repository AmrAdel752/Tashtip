using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;

namespace TASHTIP.EF.ViewModel.Production
{
    public class ProjectFinishVM
    {
        public List<BussinessGallary > BussinessGallary { get; set; } 
        public List<PurchaseRequest  > PurchaseRequest { get; set; } 
    }
}
