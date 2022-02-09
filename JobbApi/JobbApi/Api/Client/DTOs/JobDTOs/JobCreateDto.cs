using FluentValidation;
using JobbApi.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class JobCreateDto
    {
        public string Title { get; set; }

        public string Address { get; set; }

        public decimal Salary { get; set; }

        public decimal Experience { get; set; }

        public string Desc { get; set; }

        public Gender Gender { get; set; }

        public Qualification Qualification { get; set; }

        public JobType JobType { get; set; }

        public int CategoryId { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public int CompanyId { get; set; }

        public bool IsActive { get; set; }

        public DateTime Deadline { get; set; }
    }

    public class JobCreateValidator : AbstractValidator<JobCreateDto>
    {
        public JobCreateValidator()
        {
            RuleFor(x => x.Title).MaximumLength(100)
               .WithMessage("100 characters are allowed")
                .NotEmpty().NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Address).MaximumLength(150)
               .WithMessage("150 characters are allowed")
                 .NotEmpty().NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Desc).NotNull().WithMessage("Should not be empty").MaximumLength(2500)
                 .WithMessage("2500 characters are allowed");
            RuleFor(x => x.Address).MaximumLength(100).NotEmpty().NotNull()
                .WithMessage("100 characters are allowed");
            RuleFor(x => x.Salary).NotEmpty().NotNull().GreaterThan(0).WithMessage("Should not be less then 0")
                 .NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.Experience).NotEmpty().NotNull().GreaterThan(0)
              .WithMessage("Should not be less then 0");
            RuleFor(x => x.CategoryId).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.CountryId).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.CityId).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.CompanyId).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Qualification).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Gender).NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.JobType).NotNull().WithMessage("Should not be empty");


        }
    }
}
