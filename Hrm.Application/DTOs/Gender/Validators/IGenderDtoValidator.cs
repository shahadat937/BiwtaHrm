using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Gender.Validators
{

    
        public class IGenderDtoValidator : AbstractValidator<IGenderDto>
        {
            public IGenderDtoValidator()
            {
                RuleFor(b => b.GenderName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
