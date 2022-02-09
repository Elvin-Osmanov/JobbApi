using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class MemberDetailUpdateDto
    {

        public string Username { get; set; }

        public string Email { get; set; }

        public string Desc { get; set; }

        public string Address { get; set; }

        public string Occupation { get; set; }

        public string PhoneNumber { get; set; }

        public string Photo { get; set; }

        public IFormFile File { get; set; }
    }

    public class MemberDetailUpdateValidator : AbstractValidator<MemberDetailUpdateDto>
    {
        public MemberDetailUpdateValidator()
        {
            RuleFor(x => x.Username).MaximumLength(100)
                .WithMessage("100 characters are allowed")
                 .NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.Email).MaximumLength(100)
               .WithMessage("100 characters are allowed")
                 .NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.PhoneNumber).MaximumLength(50)
                 .WithMessage("50 characters are allowed");
            RuleFor(x => x.Address).MaximumLength(100)
                .WithMessage("100 characters are allowed");
            RuleFor(x => x.Occupation).MaximumLength(50)
              .WithMessage("50 characters are allowed")
                 .NotEmpty().WithMessage("Should not be empty");
            RuleFor(x => x.Desc).MaximumLength(1000)
              .WithMessage("1000 characters are allowed");
            RuleFor(x => x.Photo).MaximumLength(100)
              .WithMessage("100 characters are allowed");


        }
        
    }
}
