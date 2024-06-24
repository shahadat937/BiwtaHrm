using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Union.Validators
{
    public class IUnionDtoValidators : AbstractValidator<IUnionDto>
    {
        public IUnionDtoValidators()
        {
            RuleFor(b => b.UnionName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
