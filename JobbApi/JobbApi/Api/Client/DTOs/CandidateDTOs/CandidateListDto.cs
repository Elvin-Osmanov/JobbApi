using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs { 

    public class CandidateListDto
    {
        public List<CandidateItemDto> Candidates { get; set; }

        public int TotalCount { get; set; }
    }

    
    public class CandidateItemDto {

        public int Id { get; set; }

        public CandidateStatus Status { get; set; }

        public string JobTitle { get; set; }

        public DateTime JobDeadline { get; set; }

        public string AppUserFullName { get; set; }

        public string AppUserOccupation { get; set; }

        public string AppUserAddress { get; set; }

        public string AppUserPhoto { get; set; }

    }
}
