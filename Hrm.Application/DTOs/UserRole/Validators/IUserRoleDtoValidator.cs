using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.UserRole.Validators
{

    
        public class IUserRoleDtoValidator : AbstractValidator<IUserRoleDto>
        {
            public IUserRoleDtoValidator()
            {
                RuleFor(b => b.UserRoleName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
