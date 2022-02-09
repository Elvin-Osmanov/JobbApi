using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobGetDto
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public string Desc { get; set; }

        public decimal Experience { get; set; }

        public bool IsActive { get; set; }

        public Gender Gender { get; set; }

        public Qualification Qualification { get; set; }

        public JobType JobType { get; set; }

        public DateTime Deadline { get; set; }

        public CategoryInJobDto Category { get; set; }

        public CityInJobDto City { get; set; }

        public CountryInJobDto Country { get; set; }

        public CompanyInJobDto Company { get; set; }

        
    }

    public class CategoryInJobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CityInJobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountryInJobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyInJobDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
