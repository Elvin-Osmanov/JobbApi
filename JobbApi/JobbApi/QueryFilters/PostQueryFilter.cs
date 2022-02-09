﻿using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.QueryFilters
{
    public class PostQueryFilter
    {
        public string Keyword { get; set; }

        public JobType? JobType { get; set; }

        public decimal? MinSalary { get; set; }

        public decimal? MaxSalary { get; set; }

        public Qualification? Qualification { get; set; }

        public Gender? Gender { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int? Experience { get; set; }

        public int CategoryId { get; set; }

        public int CountryId { get; set; }
    }
}
