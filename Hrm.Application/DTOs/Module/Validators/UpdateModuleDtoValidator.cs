using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.DTOs.Modules.Validators
{
    public class UpdateModuleDtoValidator : AbstractValidator<ModuleDto>
    {
        public UpdateModuleDtoValidator()
        {
            Include(new IModuleDtoValidator());

            RuleFor(p => p.ModuleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
