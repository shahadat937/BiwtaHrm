using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingName.Validators
{
    public class UpdateTrainingNameDtoValidator : AbstractValidator<TrainingNameDto>
    {
        public UpdateTrainingNameDtoValidator()
        {
            Include(new ITrainingNameDtoValidators());

            RuleFor(x => x.TrainingNameId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
