using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Weekend.Validators
{

    
        public class IWeekendDtoValidator : AbstractValidator<IWeekendDto>
        {
            public IWeekendDtoValidator()
            {
                RuleFor(b => b.WeekendName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
