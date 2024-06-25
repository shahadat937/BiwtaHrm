using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.HairColor.Validators
{
    public class UpdateHairColorDtoValidator : AbstractValidator<HairColorDto>
    {
        public UpdateHairColorDtoValidator()
        {
            Include(new IHairColorDtoValidator());

            RuleFor(x => x.HairColorId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
