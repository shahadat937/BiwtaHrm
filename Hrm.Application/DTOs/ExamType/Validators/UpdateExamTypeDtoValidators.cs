using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ExamType.Validators
{
    public class UpdateExamTypeDtoValidators : AbstractValidator<ExamTypeDto>
    {
        public UpdateExamTypeDtoValidators()
        {
            Include(new IExamTypeDtoValidator());

            RuleFor(x => x.ExamTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
