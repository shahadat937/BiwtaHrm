using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveType.Validators
{
    public class ILeaveTypeDtoValidator:AbstractValidator<ILeaveTypeDto>
    {
        public ILeaveTypeDtoValidator() {
            RuleFor(e => e.LeaveTypeName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
