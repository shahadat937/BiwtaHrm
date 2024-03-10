using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana_Upazila.Validators
{
    public class IThana_UpazilaDtoValidators : AbstractValidator<IThana_UpazilaDto>
    {
        public IThana_UpazilaDtoValidators()
        {
            RuleFor(b => b.Thana_UpazilaName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
