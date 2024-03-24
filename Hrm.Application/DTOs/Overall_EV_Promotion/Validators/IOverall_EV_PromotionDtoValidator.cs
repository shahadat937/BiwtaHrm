using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Overall_EV_Promotion.Validators
{

    
        public class IOverall_EV_PromotionDtoValidator : AbstractValidator<IOverall_EV_PromotionDto>
        {
            public IOverall_EV_PromotionDtoValidator()
            {
                RuleFor(b => b.Overall_EV_PromotionName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
