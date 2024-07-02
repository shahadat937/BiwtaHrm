using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.AttendanceType.Validators
{
    public class IAttendanceTypeDtoValidator: AbstractValidator<IAttendanceTypeDto>
    {
        public IAttendanceTypeDtoValidator()
        {
            RuleFor(at => at.AttendanceTypeName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(at => at.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
