using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Manage.DTOs
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }

        public string Icon { get; set; }
    }

    public class CategoryCreateValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Should not be empty")
                .MaximumLength(60).WithMessage("60 characters are allowed");

            RuleFor(x => x.Icon)
               .NotEmpty().WithMessage("Should not be empty")
               .MaximumLength(250).WithMessage("60 characters are allowed");

        }
    }
}
