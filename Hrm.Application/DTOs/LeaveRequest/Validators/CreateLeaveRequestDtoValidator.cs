using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator: AbstractValidator<CreateLeaveRequestDto>
    {
        public CreateLeaveRequestDtoValidator()
        {
            Include(new ILeaveRequestDtoValidator());
            RuleFor(e => e.LeavePurpose).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
