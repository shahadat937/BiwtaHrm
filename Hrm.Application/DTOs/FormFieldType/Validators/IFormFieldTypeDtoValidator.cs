using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormFieldType.Validators
{
    public class IFormFieldTypeDtoValidator: AbstractValidator<IFormFieldTypeDto>
    {
        public IFormFieldTypeDtoValidator()
        {
            RuleFor(x => x.FieldTypeName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
