using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reward.Validators
{
    public class UpdateRewardDtoValidator : AbstractValidator<RewardDto>
    {
        public UpdateRewardDtoValidator()
        {
            Include(new IRewardDtoValidator());

            RuleFor(x => x.RewardId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
