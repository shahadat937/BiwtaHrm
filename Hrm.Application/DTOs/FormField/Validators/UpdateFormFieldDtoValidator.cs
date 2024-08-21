using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormField.Validators
{
    public class UpdateFormFieldDtoValidator: AbstractValidator<FormFieldDto>
    {
        public UpdateFormFieldDtoValidator()
        {
            RuleFor(e => e.FieldId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new IFormFieldDtoValidator());
        }
    }
}
