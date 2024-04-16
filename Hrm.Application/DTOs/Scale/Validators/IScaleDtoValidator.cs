using FluentValidation;
using Hrm.Application.DTOs.Scale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Scale.Validators
{
 
    public class IScaleDtoValidator : AbstractValidator<IScaleDto>
    {
        public IScaleDtoValidator()
        {
            RuleFor(b => b.ScaleName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
