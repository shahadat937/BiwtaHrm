using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TaskName.Validators
{

    
        public class ITaskNameDtoValidator : AbstractValidator<ITaskNameDto>
        {
            public ITaskNameDtoValidator()
            {
                RuleFor(b => b.TaskNames)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
