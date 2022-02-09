using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Manage.DTOs
{
    public class CountryCreateDto
    {
        public string Name { get; set; }
    }

    public class CountryCreateValidator : AbstractValidator<CountryCreateDto>
    {
        public CountryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Should not be empty")
                .MaximumLength(60).WithMessage("60 characters are allowed");

        }
    }
}
