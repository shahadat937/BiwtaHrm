using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Role.Validators
{
    public class CreateRoleDtoValidator : AbstractValidator<CreateRoleDto>
    {
        public CreateRoleDtoValidator() 
        {
            Include(new IRoleDtoValidator());
        }
    }
} 
