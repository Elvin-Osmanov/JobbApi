using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Entities
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Desc { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public string Phone { get; set; }

        public string Photo { get; set; }

        public List<Job> Jobs { get; set; }
    }
}
