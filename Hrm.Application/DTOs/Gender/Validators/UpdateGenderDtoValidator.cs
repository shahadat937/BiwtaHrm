using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Gender.Validators
{
    public class UpdateGenderDtoValidator : AbstractValidator<GenderDto>
    {
        public UpdateGenderDtoValidator()
        {
            Include(new IGenderDtoValidator());

            RuleFor(x => x.GenderId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
