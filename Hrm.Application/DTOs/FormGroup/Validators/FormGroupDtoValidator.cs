using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.FormGroup.Validators
{
    public class FormGroupDtoValidator : AbstractValidator<FormGroupDto>
    {
        public FormGroupDtoValidator()
        {
            Include(new IFormGroupDtoValidator());
            RuleFor(x => x.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
