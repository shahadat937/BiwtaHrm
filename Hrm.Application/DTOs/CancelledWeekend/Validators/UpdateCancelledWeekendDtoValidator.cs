using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CancelledWeekend.Validators
{
    public class UpdateCancelledWeekendDtoValidator: AbstractValidator<CancelledWeekendDto>
    {
        public UpdateCancelledWeekendDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("{PropertyName} is required"); 
            Include(new ICancelledWeekendDtoValidator());
        }
    }
}
