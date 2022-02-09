using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class CompanyCreateDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Desc { get; set; }

        public string Address { get; set; }

        public string Category { get; set; }

        public string Phone { get; set; }

        public string Photo { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }

    public class CompanyCreateValidator : AbstractValidator<CompanyCreateDto>
    {
        public CompanyCreateValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100)
               .WithMessage("100 characters are allowed")
                .NotEmpty().NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Email).MaximumLength(100)
               .WithMessage("100 characters are allowed")
                 .NotEmpty().NotNull().WithMessage("Should not be empty");
            RuleFor(x => x.Phone).NotNull().WithMessage("Should not be empty").MaximumLength(50)
                 .WithMessage("50 characters are allowed");
            RuleFor(x => x.Address).MaximumLength(100).NotEmpty().NotNull()
                .WithMessage("100 characters are allowed");
            RuleFor(x => x.Category).NotEmpty().NotNull().MaximumLength(50)
              .WithMessage("50 characters are allowed")
                 .NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.Desc).MaximumLength(1000)
              .WithMessage("1000 characters are allowed");
            RuleFor(x => x.Photo).MaximumLength(100)
              .WithMessage("100 characters are allowed");
        }
    }  
}
