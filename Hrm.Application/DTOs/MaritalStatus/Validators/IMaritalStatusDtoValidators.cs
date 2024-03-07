using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.MaritalStatus.Validators
{
    public class IMaritalStatusDtoValidators : AbstractValidator<IMaritalStatusDto>
    {
        public IMaritalStatusDtoValidators()
        {
            RuleFor(b => b.MaritalStatusName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
