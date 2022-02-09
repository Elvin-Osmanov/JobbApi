using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobGetListDto
    {
        public List<JobGetItemDto> Jobs { get; set; }
        public int TotalCount { get; set; }
    }

    public class JobGetItemDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CityName { get; set; }

        public DateTime Deadline { get; set; }

        public JobType JobType { get; set; }

        public string CompanyName { get; set; }

        public bool IsBookmarked { get; set; }

        public bool IsApplied { get; set; }


    }
}
