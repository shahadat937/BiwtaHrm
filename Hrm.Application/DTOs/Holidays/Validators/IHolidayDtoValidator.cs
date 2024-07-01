using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Holidays.Validators
{
    public class IHolidayDtoValidator: AbstractValidator<IHolidayDto>
    {
        public IHolidayDtoValidator() {
            RuleFor(e => e.HolidayTypeId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.HolidayStart).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.HolidayEnd).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
