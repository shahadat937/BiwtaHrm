using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.UserRole.Validators
{
    public class UpdateUserRoleDtoValidators : AbstractValidator<UserRoleDto>
    {
        public UpdateUserRoleDtoValidators()
        {
            Include(new IUserRoleDtoValidator());

            RuleFor(x => x.UserRoleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
