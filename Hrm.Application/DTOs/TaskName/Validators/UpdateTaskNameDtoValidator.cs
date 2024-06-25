using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TaskName.Validators
{
    public class UpdateTaskNameDtoValidator : AbstractValidator<TaskNameDto>
    {
        public UpdateTaskNameDtoValidator()
        {
            Include(new ITaskNameDtoValidator());

            RuleFor(x => x.TaskNameId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
