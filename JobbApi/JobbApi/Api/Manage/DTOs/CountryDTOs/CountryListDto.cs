using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Manage.DTOs
{
    public class CountryListDto
    {
        public List<CountryItemDto> Countries { get; set; }
        public int TotalCount { get; set; }
    }

    public class CountryItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
