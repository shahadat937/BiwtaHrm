using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Religion.Validators
{
    public class CreateReligionDtoValidator : AbstractValidator<CreateReligionDto>
    {
        public CreateReligionDtoValidator()
        {
            Include(new IReligionDtoValidator());
        }
    }
}
