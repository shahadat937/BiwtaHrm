using FluentValidation;
using Hrm.Application.DTOs.Religion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Ward.Validators
{
    public class IWardDtoValidator : AbstractValidator<IWardDto>
    {
        public IWardDtoValidator()
        {
            RuleFor(b => b.WardName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}