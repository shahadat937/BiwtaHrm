using FluentValidation;
using Hrm.Application.DTOs.ScaleGradeView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ScaleGradeView.Validators
{
 
    public class IScaleGradeViewDtoValidator : AbstractValidator<IScaleGradeViewDto>
    {
        public IScaleGradeViewDtoValidator()
        {
            RuleFor(b => b.ScaleName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
