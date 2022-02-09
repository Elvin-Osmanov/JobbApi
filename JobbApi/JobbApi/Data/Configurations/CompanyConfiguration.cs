using JobbApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Configurations
{
    public class CompanyConfiguration:IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(60).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Photo).HasMaxLength(100);
            builder.Property(x => x.Category).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Desc).HasMaxLength(1500);


        }
    }
}
