using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Shift.Validators
{

    
        public class IShiftDtoValidator : AbstractValidator<IShiftDto>
        {
            public IShiftDtoValidator()
            {
                RuleFor(b => b.ShiftName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
