using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class MemberLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class MemberLoginDtoValidator : AbstractValidator<MemberLoginDto>
    {
        public MemberLoginDtoValidator()
        {
            RuleFor(x => x.Email).MaximumLength(100).NotEmpty().WithMessage("Should not be empty").NotNull().WithMessage("Email or Password is correct!");
            RuleFor(x => x.Password).MaximumLength(20).NotNull().NotEmpty().WithMessage("Email or Password is correct!"); 
        }
    }
}
