using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EyesColor.Validators
{

    
        public class IEyesColorDtoValidator : AbstractValidator<IEyesColorDto>
        {
            public IEyesColorDtoValidator()
            {
                RuleFor(b => b.EyesColorName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
