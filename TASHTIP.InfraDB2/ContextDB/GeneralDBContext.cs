 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Production;

namespace TASHTIP.InfraDB.ContextDB
{
    public class GeneralDBContext : DbContext
    {
        public GeneralDBContext(DbContextOptions<GeneralDBContext> options) : base(options)
        {
             
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 

            //modelBuilder.Entity<PurchaseRequest_Main>().HasKey(PRM => new { PRM.PR_NO , PRM.Year  });
            //modelBuilder.Entity<PurchaseRequest_Sub>().HasKey(PRS => new { PRS.PR_NO , PRS.Year, PRS.Serial  });

             
        }


        //Production
        public DbSet<City> City { get; set; }
        public DbSet<ServicesType> ServicesType { get; set; }
        public DbSet<SupervisorEnginners > SupervisorEnginners { get; set; }
        public DbSet<BussinessGallary > BussinessGallary { get; set; }
        public DbSet<FilterGallary> FilterGallary { get; set; }
        public DbSet<PurchaseRequest> PurchaseRequest { get; set; }



    }
}
