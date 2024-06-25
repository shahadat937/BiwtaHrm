using FluentValidation;
using Hrm.Application.DTOs.TaskName.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.TaskName.Validators
{
    public class CreateTaskNameDtoValidator : AbstractValidator<CreateTaskNameDto>
    {
        public CreateTaskNameDtoValidator()
        {
            Include(new ITaskNameDtoValidator());
        }
    }
}
