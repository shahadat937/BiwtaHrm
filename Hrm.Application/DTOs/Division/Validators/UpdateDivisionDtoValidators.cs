using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Division.Validators
{
    public class UpdateDivisionDtoValidators : AbstractValidator<DivisionDto>
    {
        public UpdateDivisionDtoValidators()
        {
            Include(new IDivisionDtoValidator());

            RuleFor(x => x.DivisionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
