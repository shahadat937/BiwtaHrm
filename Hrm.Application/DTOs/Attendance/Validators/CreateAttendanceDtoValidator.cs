using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Attendance.Validators
{
    public class CreateAttendanceDtoValidator: AbstractValidator<CreateAttendanceDto>
    {
        public CreateAttendanceDtoValidator()
        {
            Include(new IAttendanceDtoValidator());
        }
    }
}
