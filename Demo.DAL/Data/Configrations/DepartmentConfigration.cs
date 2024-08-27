using Demo.DAL.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data.Configrations
{
    internal class DepartmentConfigration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department>D)
        {
            D.Property(d => d.Id).UseIdentityColumn(10,5);

            D.Property(d => d.Name)
                .IsRequired();

            D.Property(d => d.Code)
                .IsRequired();

        }
    }
}
