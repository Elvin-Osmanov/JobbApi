using JobbApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(60);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.UserName).HasMaxLength(25);
            builder.Property(x => x.Photo).HasMaxLength(100);
            builder.Property(x => x.Occupation).HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).HasMaxLength(50);
            builder.Property(x => x.Address).HasMaxLength(100);
            builder.Property(x => x.Desc).HasMaxLength(1500);


        }
    }
}
