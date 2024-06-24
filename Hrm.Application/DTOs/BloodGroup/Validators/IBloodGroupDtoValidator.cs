using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BloodGroup.Validators
{

    
        public class IBloodGroupDtoValidator : AbstractValidator<IBloodGroupDto>
        {
            public IBloodGroupDtoValidator()
            {
                RuleFor(b => b.BloodGroupName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
