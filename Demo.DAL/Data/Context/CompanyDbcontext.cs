using Demo.DAL.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Context
{
    public class CompanyDbcontext : IdentityDbContext<ApplicationUser>
    {

        public CompanyDbcontext(DbContextOptions<CompanyDbcontext> options) : base(options)
        {

        }

       ///protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       ///{
       ///    optionsBuilder.UseSqlServer("Server=.;Database=MVCProject;Trusted_Connection=True;");
       ///}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }


    }
}
