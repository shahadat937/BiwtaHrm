using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.WeekDay.Validators
{
    public class UpdateWeekDayDtoValidator : AbstractValidator<WeekDayDto>
    {
        public UpdateWeekDayDtoValidator()
        {
            Include(new IWeekDayDtoValidator());

            RuleFor(x => x.WeekDayId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
