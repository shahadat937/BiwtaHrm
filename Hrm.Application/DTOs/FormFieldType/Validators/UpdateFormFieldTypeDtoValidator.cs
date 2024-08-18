using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormFieldType.Validators
{
    public class UpdateFormFieldTypeDtoValidator: AbstractValidator<FormFieldTypeDto>
    {
        public UpdateFormFieldTypeDtoValidator()
        {
            RuleFor(x => x.FieldTypeId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new IFormFieldTypeDtoValidator());
        }
    }
}
