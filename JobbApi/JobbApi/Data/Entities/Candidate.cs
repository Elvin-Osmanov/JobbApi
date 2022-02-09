using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Entities
{
    public class Candidate:BaseEntity
    {
        public int JobId { get; set; }

        public Job Job { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public CandidateStatus Status { get; set; }


    }
}
