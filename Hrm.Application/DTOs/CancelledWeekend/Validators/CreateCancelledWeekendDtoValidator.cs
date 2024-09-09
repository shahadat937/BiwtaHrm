using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CancelledWeekend.Validators
{
    public class CreateCancelledWeekendDtoValidator: AbstractValidator<CreateCancelledWeekendDto>
    {
        public CreateCancelledWeekendDtoValidator()
        {
            Include(new ICancelledWeekendDtoValidator());
        }
    }
}
