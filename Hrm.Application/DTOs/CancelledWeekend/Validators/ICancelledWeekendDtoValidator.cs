using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CancelledWeekend.Validators
{
    public class ICancelledWeekendDtoValidator: AbstractValidator<ICancelledWeekend>
    {
        public ICancelledWeekendDtoValidator()
        {
            RuleFor(x => x.CancelDate).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
