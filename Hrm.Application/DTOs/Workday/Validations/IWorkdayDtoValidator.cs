using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Workday.Validations
{
    public class IWorkdayDtoValidator : AbstractValidator<IWorkdayDto>
    {
        public IWorkdayDtoValidator() {
            RuleFor(x => x.YearId).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.WorkdayId).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
