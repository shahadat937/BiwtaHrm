using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HairColor.Validators
{

    
        public class IHairColorDtoValidator : AbstractValidator<IHairColorDto>
        {
            public IHairColorDtoValidator()
            {
                RuleFor(b => b.HairColorName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
