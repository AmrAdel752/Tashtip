 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TASHTIP.EF.Entities.Employee;

namespace TASHTIP.InfraDB
{
    public class EmployeeDBContext  : IdentityDbContext<ApplicationUser>
    { 
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<ApplicationUser> Users { get; set; }

    }
}
