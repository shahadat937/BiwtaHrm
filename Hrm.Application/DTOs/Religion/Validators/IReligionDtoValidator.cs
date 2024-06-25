using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Religion.Validators
{

    
        public class IReligionDtoValidator : AbstractValidator<IReligionDto>
        {
            public IReligionDtoValidator()
            {
                RuleFor(b => b.ReligionName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
