using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.WeekDay.Validators
{
    public class CreateWeekDayDtoValidator : AbstractValidator<CreateWeekDayDto>
    {
        public CreateWeekDayDtoValidator()
        {
            Include(new IWeekDayDtoValidator());
        }
    }
}
