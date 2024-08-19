using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form.Validators
{
    public class FormDtoValidator: AbstractValidator<FormDto>
    {
        public FormDtoValidator() {
            Include(new IFormDtoValidator());
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
