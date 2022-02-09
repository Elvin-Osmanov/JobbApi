using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobListDto
    {
        public List<JobItemDto> Jobs { get; set; }
        public int TotalCount { get; set; }
    }

    public class JobItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public JobType JobType { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsActive { get; set; }

        public string CompanyName { get; set; }

        public string CategoryName { get; set; }

        public string CountryName { get; set; }

        public string CityName { get; set; }
    }
}
