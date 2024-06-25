using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.appraisalFormType.Validators
{
    public class IappraisalFormTypeDtoValidator : AbstractValidator<IappraisalFormTypeDto>
    {
        public IappraisalFormTypeDtoValidator()
        {
            RuleFor(b=>b.appraisalFormTypeName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
