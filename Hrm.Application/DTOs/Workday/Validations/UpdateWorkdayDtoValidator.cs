using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Workday.Validations
{
    public class UpdateWorkdayDtoValidator : AbstractValidator<WorkdayDto>
    {
        public UpdateWorkdayDtoValidator() {
            Include(new IWorkdayDtoValidator());
            RuleFor(x => x.WorkdayId).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
