using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Overall_EV_Promotion.Validators
{
    public class CreateOverall_EV_PromotionDtoValidator : AbstractValidator<CreateOverall_EV_PromotionDto>
    {
        public CreateOverall_EV_PromotionDtoValidator()
        {
            Include(new IOverall_EV_PromotionDtoValidator());
        }
    }
}
