using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DayType.Validators
{
    public class IDayTypeDtoValidator: AbstractValidator<IDayTypeDto>
    {
        public IDayTypeDtoValidator()
        {
            RuleFor(dt => dt.DayTypeName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
