using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TrainingType.Validators
{
    public class CreateTrainingTypeDtoValidator : AbstractValidator<CreateTrainingTypeDto>
    {
        public CreateTrainingTypeDtoValidator() { 
            Include(new ITrainingTypeDtoValidator());
        }
    }
}
