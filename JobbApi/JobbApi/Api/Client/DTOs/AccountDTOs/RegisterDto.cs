using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Should not be empty")
                .MaximumLength(60).WithMessage("60 characters are allowed");
            RuleFor(x => x.Email).MaximumLength(100).WithMessage("100 characters are allowed")
                 .NotEmpty().WithMessage("Should not be empty");
             
            RuleFor(x => x.Password).NotEmpty().NotNull().MaximumLength(20).WithMessage("Should not be empty");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("password and confirm password are not same");
        }
    }
}
