using JobbApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Configurations
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
           
            builder.HasOne(j => j.Job).WithMany(c => c.Candidates).HasForeignKey(k=>k.JobId);
            builder.HasOne(j => j.AppUser).WithMany(c => c.Candidates).HasForeignKey(k => k.AppUserId);
        }
    }
}
