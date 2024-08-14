using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequestDtoValidator: AbstractValidator<ILeaveRequestDto>
    {
        public ILeaveRequestDtoValidator()
        {
            RuleFor(e => e.EmpId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.FromDate).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.ToDate).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.LeaveTypeId).NotEmpty().WithMessage("{PropertyName} is required");
        }

    }
}
