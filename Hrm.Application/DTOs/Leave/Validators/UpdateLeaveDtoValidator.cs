using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Leave.Validators
{
    public class UpdateLeaveDtoValidator : AbstractValidator<LeaveDto>
    {
        public UpdateLeaveDtoValidator()
        {
            Include(new ILeaveDtoValidator());

            RuleFor(x => x.LeaveId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
