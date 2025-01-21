using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.FormGroup.Validators
{
    public class IFormGroupDtoValidator : AbstractValidator<IFormGroupDto>
    {
        public IFormGroupDtoValidator()
        {
            RuleFor(x => x.FormFieldId).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.ParentFieldId).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FormFieldId).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
