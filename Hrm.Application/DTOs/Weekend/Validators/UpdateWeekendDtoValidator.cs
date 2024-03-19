using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Weekend.Validators
{
    public class UpdateWeekendDtoValidator : AbstractValidator<WeekendDto>
    {
        public UpdateWeekendDtoValidator()
        {
            Include(new IWeekendDtoValidator());

            RuleFor(x => x.WeekendId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
