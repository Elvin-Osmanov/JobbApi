using FluentValidation;
using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class CandidateCreateDto
    {
        public string AppUserId { get; set; }

        public int JobId { get; set; }

        public CandidateStatus Status { get; set; }
    }

    public class CandidateCreateValidator : AbstractValidator<CandidateCreateDto> {

        public CandidateCreateValidator()
        {
            RuleFor(x => x.JobId).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.AppUserId).NotNull().WithMessage("Should not be empty");
        }
    
    } 
}
