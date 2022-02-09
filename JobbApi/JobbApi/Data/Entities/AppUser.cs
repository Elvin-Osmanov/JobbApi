using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Data.Entities
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }

        public string Occupation { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public string Desc { get; set; }

        public bool IsAdmin { get; set; }

        public List<Candidate> Candidates { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
