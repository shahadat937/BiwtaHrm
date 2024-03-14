using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Shift.Validators
{
    public class UpdateShiftDtoValidator : AbstractValidator<ShiftDto>
    {
        public UpdateShiftDtoValidator()
        {
            Include(new IShiftDtoValidator());

            RuleFor(x => x.ShiftId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
