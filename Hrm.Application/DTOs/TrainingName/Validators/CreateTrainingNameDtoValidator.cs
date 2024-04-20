using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingName.Validators
{
    public class CreateTrainingNameDtoValidator : AbstractValidator<CreateTrainingNameDto>
    {
        public CreateTrainingNameDtoValidator()
        {
            Include(new ITrainingNameDtoValidators());
        }
    }
}
