using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Occupation.Validators
{
    public class UpdateOccupationDtoValidators : AbstractValidator<OccupationDto>
    {
        public UpdateOccupationDtoValidators()
        {
            Include(new IOccupationDtoValidator());

            RuleFor(x => x.OccupationId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
