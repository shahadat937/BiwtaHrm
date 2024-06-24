using FluentValidation;
using Hrm.Application.DTOs.GradeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeClass.Validators
{
    public class IGradeClassDtoValidator : AbstractValidator<IGradeClassDto>
    {
        public IGradeClassDtoValidator()
        {
            RuleFor(b => b.GradeClassName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}