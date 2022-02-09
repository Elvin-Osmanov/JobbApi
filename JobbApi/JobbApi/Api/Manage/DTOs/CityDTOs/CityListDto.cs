using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Manage.DTOs
{
    public class CityListDto
    {
        public List<CityItemDto> Cities { get; set; }
        public int TotalCount { get; set; }
    }

    public class CityItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

