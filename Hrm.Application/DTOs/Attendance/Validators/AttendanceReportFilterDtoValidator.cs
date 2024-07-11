using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance.Validators
{
    public class AttendanceReportFilterDtoValidator: AbstractValidator<AttendanceReportFilterDto>
    {
        public AttendanceReportFilterDtoValidator()
        {
            RuleFor(atf => atf.From).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(atf => atf.To).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
