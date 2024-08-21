using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormField.Validators
{
    public class IFormFieldDtoValidator: AbstractValidator<IFormFieldDto>
    {
        public IFormFieldDtoValidator()
        {
            RuleFor(e => e.FieldName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.FieldTypeId).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
