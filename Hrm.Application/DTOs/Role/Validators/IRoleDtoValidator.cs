using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Role.Validators
{
    public class IRoleDtoValidator : AbstractValidator<IRoleDto>
    {
        public IRoleDtoValidator()
        {
            RuleFor(p => p.RoleName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");

            //RuleFor(p => p.LoweredRoleName)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .NotNull()
            //    .MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}
