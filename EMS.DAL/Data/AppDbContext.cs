using EMS.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.DAL.Data
{
    public class AppDbContext :  IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Allowance> Allowances { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Appreciation> Appreciations { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }


    }
}
