using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configrations
{
    internal class EmployeeConfigrations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> E)
        {
            E.Property(e => e.Salary)
                .HasColumnType("decimal(18,2)");

            E.HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.Dept_Id)
                .OnDelete(DeleteBehavior.Cascade);

            E.Property(e => e.Address)
                .IsRequired(true);
            E.Property(e => e.Name)
                .IsRequired(true)
                .HasMaxLength(50);

        }
    }
}
