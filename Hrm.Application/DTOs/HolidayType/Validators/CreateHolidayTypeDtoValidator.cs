using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HolidayType.Validators
{
    public class CreateHolidayTypeDtoValidator : AbstractValidator<CreateHolidayTypeDto>
    {
        public CreateHolidayTypeDtoValidator()
        {
            Include(new IHolidayTypeDtoValidator());
        }
    }
}
