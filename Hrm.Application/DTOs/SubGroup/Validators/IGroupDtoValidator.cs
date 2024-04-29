using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Group.Validators
{

    
        public class IGroupDtoValidator : AbstractValidator<IGroupDto>
        {
            public IGroupDtoValidator()
            {
                RuleFor(b => b.GroupName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
