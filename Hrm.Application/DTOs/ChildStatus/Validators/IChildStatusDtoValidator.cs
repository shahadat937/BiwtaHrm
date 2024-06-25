using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ChildStatus.Validators
{

    
        public class IChildStatusDtoValidator : AbstractValidator<IChildStatusDto>
        {
            public IChildStatusDtoValidator()
            {
                RuleFor(b => b.ChildStatusName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
