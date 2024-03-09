using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BloodGroup.Validators
{
    public class UpdateBloodGroupDtoValidators : AbstractValidator<BloodGroupDto>
    {
        public UpdateBloodGroupDtoValidators()
        {
            Include(new IBloodGroupDtoValidator());

            RuleFor(x => x.BloodGroupId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
