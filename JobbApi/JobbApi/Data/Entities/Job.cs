using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Entities
{
    public class Job:BaseEntity
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public decimal Experience { get; set; }

        public string Desc { get; set; }

        public Gender Gender { get; set; }

        public Qualification Qualification { get; set; }

        public JobType JobType { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public City City { get; set; }

        public int CityId { get; set; }

        public Country Country { get; set; }

        public int CountryId { get; set; }

        public Company Company {get; set; }

        public int CompanyId { get; set; }

        public bool IsActive { get; set; }

        public bool IsBookmarked { get; set; }

        public bool IsApplied { get; set; }

        public DateTime Deadline { get; set; }

        public List<Candidate> Candidates { get; set; }

    }
}
