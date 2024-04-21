using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HairColor.Validators
{
    public class CreateHairColorDtoValidator : AbstractValidator<CreateHairColorDto>
    {
        public CreateHairColorDtoValidator()
        {
            Include(new IHairColorDtoValidator());
        }
    }
}
