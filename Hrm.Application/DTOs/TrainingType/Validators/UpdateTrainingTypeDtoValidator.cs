using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingType.Validators
{
    public class UpdateTrainingTypeDtoValidator : AbstractValidator<TrainingTypeDto>
    {
        public UpdateTrainingTypeDtoValidator()
        {
            Include(new ITrainingTypeDtoValidator());

            RuleFor(x => x.TrainingTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}

