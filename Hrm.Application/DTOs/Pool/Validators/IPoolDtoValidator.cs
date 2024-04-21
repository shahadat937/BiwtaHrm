using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Pool.Validators
{

    
        public class IPoolDtoValidator : AbstractValidator<IPoolDto>
        {
            public IPoolDtoValidator()
            {
                RuleFor(b => b.PoolName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
