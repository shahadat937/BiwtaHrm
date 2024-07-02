using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceStatus.Validators
{
    public class CreateAttendanceStatusDtoValidator:AbstractValidator<CreateAttendanceStatusDto>
    {
        public CreateAttendanceStatusDtoValidator()
        {
            Include(new IAttendanceStatusDtoValidator());
        }
    }
}
