using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Institute.Validators
{
    public class IInstituteDtoValidator :AbstractValidator<IInstituteDto>
    {
        public IInstituteDtoValidator()
        {
            RuleFor(b => b.InstituteName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}
