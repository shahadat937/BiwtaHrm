using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Shift.Validators
{
    public class CreateShiftDtoValidator : AbstractValidator<CreateShiftDto>
    {
        public CreateShiftDtoValidator()
        {
            Include(new IShiftDtoValidator());
        }
    }
}
