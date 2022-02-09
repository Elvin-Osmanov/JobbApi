using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobApplyDto
    {
        public int Id { get; set; }

        public bool IsApplied { get; set; }
    }
}
