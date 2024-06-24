using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Upazila.Validators
{
    public class IUpazilaDtoValidators : AbstractValidator<IUpazilaDto>
    {
        public IUpazilaDtoValidators()
        {
            RuleFor(b => b.UpazilaName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
