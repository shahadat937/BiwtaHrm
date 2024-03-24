using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Overall_EV_Promotion.Validators
{
    public class UpdateOverall_EV_PromotionDtoValidator : AbstractValidator<Overall_EV_PromotionDto>
    {
        public UpdateOverall_EV_PromotionDtoValidator()
        {
            Include(new IOverall_EV_PromotionDtoValidator());

            RuleFor(x => x.Overall_EV_PromotionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
