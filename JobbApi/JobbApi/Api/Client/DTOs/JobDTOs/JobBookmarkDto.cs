using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobBookmarkDto
    {

        public int Id { get; set; }

        public bool IsBookmarked { get; set; }
    }
}
