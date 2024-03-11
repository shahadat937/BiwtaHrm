using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana.Validators
{
    public class IThanaDtoValidators : AbstractValidator<IThanaDto>
    {
        public IThanaDtoValidators()
        {
            RuleFor(b => b.ThanaName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
