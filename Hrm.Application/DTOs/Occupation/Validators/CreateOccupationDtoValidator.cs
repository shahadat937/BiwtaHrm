using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Occupation.Validators
{
    public class CreateOccupationDtoValidator : AbstractValidator<CreateOccupationDto>
    {
        public CreateOccupationDtoValidator()
        {
            Include(new IOccupationDtoValidator());
        }
    }
}
