using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Result.Validators
{

    
        public class IResultDtoValidator : AbstractValidator<IResultDto>
        {
            public IResultDtoValidator()
            {
                RuleFor(b => b.ResultName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
