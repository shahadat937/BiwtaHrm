using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Gender.Validators
{
    public class CreateGenderDtoValidator : AbstractValidator<CreateGenderDto>
    {
        public CreateGenderDtoValidator()
        {
            Include(new IGenderDtoValidator());
        }
    }
}
