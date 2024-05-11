using FluentValidation;
using Hrm.Application.DTOs.WeekDay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.WeekDay.Validators
{

    
        public class IWeekDayDtoValidator : AbstractValidator<IWeekDayDto>
        {
            public IWeekDayDtoValidator()
            {
                RuleFor(b => b.WeekDayName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
