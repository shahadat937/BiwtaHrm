using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Holidays.Validators
{
    public class CreateHolidayDtoValidator: AbstractValidator<CreateHolidayDto>
    {
        public CreateHolidayDtoValidator()
        {
            Include(new IHolidayDtoValidator());
        }
    }
}
