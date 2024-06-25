using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Reward.Validators
{
    public class CreateRewardDtoValidator : AbstractValidator<CreateRewardDto>
    {
        public CreateRewardDtoValidator()
        {
            Include(new IRewardDtoValidator());
        }
    }
}
