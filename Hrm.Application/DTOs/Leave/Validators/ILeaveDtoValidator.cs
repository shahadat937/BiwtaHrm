using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Leave.Validators
{

    
        public class ILeaveDtoValidator : AbstractValidator<ILeaveDto>
        {
            public ILeaveDtoValidator()
            {
                RuleFor(b => b.LeaveName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
