using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reward.Validators
{

    
        public class IRewardDtoValidator : AbstractValidator<IRewardDto>
        {
            public IRewardDtoValidator()
            {
                RuleFor(b => b.RewardName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
