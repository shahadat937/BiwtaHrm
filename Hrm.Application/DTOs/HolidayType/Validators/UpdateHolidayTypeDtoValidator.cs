using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HolidayType.Validators
{
    public class UpdateHolidayTypeDtoValidator : AbstractValidator<HolidayTypeDto>
    {
        public UpdateHolidayTypeDtoValidator()
        {
            Include(new IHolidayTypeDtoValidator());

            RuleFor(x => x.HolidayTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
