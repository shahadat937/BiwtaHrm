using FluentValidation;
using Hrm.Application.DTOs.TrainingType.Validators;
using Hrm.Application.DTOs.TrainingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PromotionType.Validators
{
    public class UpdatePromotionTypeDtoValidator : AbstractValidator<PromotionTypeDto>
    {
        public UpdatePromotionTypeDtoValidator()
        {
            Include(new IPromotionTypeDtoValidator());

            RuleFor(x => x.PromotionTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}