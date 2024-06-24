using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingType.Validators
{
    public class ITrainingTypeDtoValidator : AbstractValidator<ITrainingTypeDto>
    {
        public ITrainingTypeDtoValidator()
        {
            RuleFor(b => b.TrainingTypeName)
                   .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
