using FluentValidation;
using Hrm.Application.DTOs.GradeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Grade.Validators
{
    public class IGradeDtoValidator : AbstractValidator<IGradeDto>
    {
        public IGradeDtoValidator()
        {
            RuleFor(b => b.GradeName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}