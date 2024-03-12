using FluentValidation;
using Hrm.Application.DTOs.TrainingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.PromotionType.Validators
{
    public class IPromotionTypeDtoValidator : AbstractValidator<IPromotionTypeDto>
    {
        public IPromotionTypeDtoValidator()
        {
            RuleFor(b => b.PromotionTypeName)
                   .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}