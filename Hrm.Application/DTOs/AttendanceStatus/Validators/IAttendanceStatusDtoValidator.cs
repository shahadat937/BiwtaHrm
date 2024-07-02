using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceStatus.Validators
{
    public class IAttendanceStatusDtoValidator: AbstractValidator<IAttendanceStatusDto>
    {
        public IAttendanceStatusDtoValidator()
        {
            RuleFor(s => s.AttendanceStatusName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
