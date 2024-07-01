using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DayType.Validators
{
    public class CreateDayTypeDtoValidator:AbstractValidator<CreateDayTypeDto>
    {
        public CreateDayTypeDtoValidator()
        {
            Include(new IDayTypeDtoValidator());
        }
    }
}
