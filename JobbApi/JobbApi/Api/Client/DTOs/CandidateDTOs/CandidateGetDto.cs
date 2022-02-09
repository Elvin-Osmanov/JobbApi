using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class CandidateGetDto
    {
        public int Id { get; set; }

        public CandidateStatus Status { get; set; }

        public JobInCandidateDto Job { get; set; }

        public AppUserInCandidateDto AppUser { get; set; }
    }

    public class JobInCandidateDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Deadline { get; set; }

    }

    public class AppUserInCandidateDto {

        public string Id { get; set; }

        public string FullName { get; set; }

        public string Photo { get; set; }

        public string Occupation { get; set; }

        public string Address { get; set; }

    }
}
