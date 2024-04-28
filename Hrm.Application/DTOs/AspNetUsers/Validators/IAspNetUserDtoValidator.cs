using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AspNetUsers.Validators
{
    public class IAspNetUserDtoValidator : AbstractValidator<IAspNetUserDto>
    {
        public IAspNetUserDtoValidator()
        {
            RuleFor(b => b.UserName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
