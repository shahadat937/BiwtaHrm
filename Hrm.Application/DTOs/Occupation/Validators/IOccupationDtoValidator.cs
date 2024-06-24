using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Occupation.Validators
{

    
        public class IOccupationDtoValidator : AbstractValidator<IOccupationDto>
        {
            public IOccupationDtoValidator()
            {
                RuleFor(b => b.OccupationName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
