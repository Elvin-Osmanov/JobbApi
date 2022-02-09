using JobbApi.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Services
{
    public interface IJwtService
    {
        string Generate(AppUser user, IList<string> roles);
    }
}
