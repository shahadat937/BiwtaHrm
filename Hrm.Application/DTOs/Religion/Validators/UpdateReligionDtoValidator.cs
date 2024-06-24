using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Religion.Validators
{
    public class UpdateReligionDtoValidator : AbstractValidator<ReligionDto>
    {
        public UpdateReligionDtoValidator()
        {
            Include(new IReligionDtoValidator());

            RuleFor(x => x.ReligionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
