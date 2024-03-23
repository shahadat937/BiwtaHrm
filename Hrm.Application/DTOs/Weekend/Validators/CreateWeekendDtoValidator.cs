using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Weekend.Validators
{
    public class CreateWeekendDtoValidator : AbstractValidator<CreateWeekendDto>
    {
        public CreateWeekendDtoValidator()
        {
            Include(new IWeekendDtoValidator());
        }
    }
}
