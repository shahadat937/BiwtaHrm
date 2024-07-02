using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceType.Validators
{
    public class CreateAttendanceTypeDtoValidator:AbstractValidator<CreateAttendanceTypeDto>
    {
        public CreateAttendanceTypeDtoValidator()
        {
            Include(new IAttendanceTypeDtoValidator());
        }
    }
}
