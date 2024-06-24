using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.District.Validators
{
    public class IDistrictDtoValidator :AbstractValidator<IDistrictDto>
    {
        public IDistrictDtoValidator()
        {
            RuleFor(b => b.DistrictName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}
