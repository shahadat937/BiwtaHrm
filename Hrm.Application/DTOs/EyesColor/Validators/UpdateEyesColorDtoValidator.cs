using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EyesColor.Validators
{
    public class UpdateEyesColorDtoValidator : AbstractValidator<EyesColorDto>
    {
        public UpdateEyesColorDtoValidator()
        {
            Include(new IEyesColorDtoValidator());

            RuleFor(x => x.EyesColorId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
