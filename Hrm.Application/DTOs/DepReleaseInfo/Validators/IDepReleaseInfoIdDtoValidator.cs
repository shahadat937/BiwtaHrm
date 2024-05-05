using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DepReleaseInfo.Validators
{


    public class IDepReleaseInfoDtoValidator : AbstractValidator<IDepReleaseInfoDto>
    {
        public IDepReleaseInfoDtoValidator()
        {
            RuleFor(b => b.DepClearance)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}
