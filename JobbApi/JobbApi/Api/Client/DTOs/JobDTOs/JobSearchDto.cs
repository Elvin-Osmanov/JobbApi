using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobSearchDto
    {
        public List<JobSearchItemDto> SearchedJobs { get; set; }

        public int TotalCount { get; set; }
    }

    public class JobSearchItemDto
    {
        public string? Title { get; set; }

        public string? CompanyName { get; set; }

        public string? Keyword { get; set; }

        public int CountryId { get; set; }
    }
}
