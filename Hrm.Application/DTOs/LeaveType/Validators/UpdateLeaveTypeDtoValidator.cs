using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveType.Validators
{
    public class UpdateLeaveTypeDtoValidator: AbstractValidator<CreateLeaveTypeDto>
    {
        public UpdateLeaveTypeDtoValidator() {
            Include(new ILeaveTypeDtoValidator());
            RuleFor(e => e.LeaveTypeId).NotNull().WithMessage("{PropertyName} is required");

        }
    }
}
