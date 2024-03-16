using FluentValidation;
using Hrm.Application.DTOs.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeType.Validators
{
    public class IGradeTypeDtoValidator : AbstractValidator<IGradeTypeDto>
    {
        public IGradeTypeDtoValidator()
        {
            RuleFor(b => b.GradeTypeName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}
