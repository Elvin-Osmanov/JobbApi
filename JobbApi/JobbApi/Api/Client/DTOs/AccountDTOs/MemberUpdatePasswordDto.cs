using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobbApi.Api.Client.DTOs
{
    public class MemberUpdatePasswordDto
    {


        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }

    public class MemberUpdatePasswordValidator : AbstractValidator<MemberUpdatePasswordDto>
    {
        public MemberUpdatePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword).MaximumLength(20).NotNull().NotEmpty();
            RuleFor(x => x.CurrentPassword).NotEqual(x => x.NewPassword).WithMessage("current password and new password cannot be the same");
            RuleFor(x => x.NewPassword).MaximumLength(20).NotNull().NotEmpty();
            RuleFor(x => x.NewPassword).Equal(x => x.ConfirmNewPassword).WithMessage("new password and new confirm password are not same");
        }
    }
}
