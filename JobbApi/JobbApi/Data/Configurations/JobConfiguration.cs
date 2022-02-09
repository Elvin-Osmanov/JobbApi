using JobbApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.Property(j => j.Title).HasMaxLength(100).IsRequired();
            builder.Property(j => j.Address).IsRequired().HasMaxLength(150);
            builder.Property(j => j.Desc).HasMaxLength(2500).IsRequired();
            builder.Property(j => j.Salary).HasColumnType("decimal(18,2)");
            builder.Property(j => j.Experience).HasColumnType("decimal(18,2)");
            builder.HasOne(j => j.Category).WithMany(j => j.Jobs);
            builder.HasOne(j => j.Country).WithMany(j => j.Jobs);
            builder.HasOne(j => j.Company).WithMany(j => j.Jobs);
            builder.HasOne(j => j.City).WithMany(j => j.Jobs);


        }
    }
}
