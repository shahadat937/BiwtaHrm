using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HolidayType.Validators
{

    
        public class IHolidayTypeDtoValidator : AbstractValidator<IHolidayTypeDto>
        {
            public IHolidayTypeDtoValidator()
            {
                RuleFor(b => b.HolidayTypeName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
