using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DayType.Validators
{
    public class UpdateDayTypeDtoValidator : AbstractValidator<CreateDayTypeDto>
    {
        public UpdateDayTypeDtoValidator()
        {
            Include(new IDayTypeDtoValidator());
            RuleFor(dt => dt.DayTypeId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
