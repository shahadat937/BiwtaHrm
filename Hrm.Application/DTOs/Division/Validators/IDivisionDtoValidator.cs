using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Division.Validators
{

    
        public class IDivisionDtoValidator : AbstractValidator<IDivisionDto>
        {
            public IDivisionDtoValidator()
            {
                RuleFor(b => b.DivisionName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
