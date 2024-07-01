using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Holidays.Validators
{
    public class UpdateHolidayDtoValidator:AbstractValidator<CreateHolidayDto>
    {
        public UpdateHolidayDtoValidator() {
            Include(new IHolidayDtoValidator());
            RuleFor(e => e.HolidayId).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
