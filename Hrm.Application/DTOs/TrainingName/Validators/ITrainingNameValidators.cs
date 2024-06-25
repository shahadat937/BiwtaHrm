using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingName.Validators
{
    public class ITrainingNameDtoValidators : AbstractValidator<ITrainingNameDto>
    {
        public ITrainingNameDtoValidators()
        {
            RuleFor(b => b.TrainingNames)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
