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
    public class CreatePromotionTypeDtoValidator : AbstractValidator<CreatePromotionTypeDto>
    {
        public CreatePromotionTypeDtoValidator()
        {
            Include(new IPromotionTypeDtoValidator());
        }
    }
}
