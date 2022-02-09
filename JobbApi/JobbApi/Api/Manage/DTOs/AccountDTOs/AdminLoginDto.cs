using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Manage.DTOs
{
    public class AdminLoginDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class AdminLoginDtoValidator : AbstractValidator<AdminLoginDto>
    {
        public AdminLoginDtoValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty().MaximumLength(35);
            RuleFor(x => x.Password).NotNull().NotEmpty().MaximumLength(35);
        }
    }
}
