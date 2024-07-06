using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance.Validators
{
    public class IAttendanceDtoValidator : AbstractValidator<IAttendanceDto>
    {
        public IAttendanceDtoValidator()
        {
            RuleFor(at => at.AttendanceDate).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(at => at.EmpId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
