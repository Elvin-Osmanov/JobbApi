using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Entities
{
    public class City:BaseEntity
    {
        public string Name { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
