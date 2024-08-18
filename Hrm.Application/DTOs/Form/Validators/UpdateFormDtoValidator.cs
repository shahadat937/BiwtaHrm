using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form.Validators
{
    public class UpdateFormDtoValidator: AbstractValidator<FormDto>
    {
        public UpdateFormDtoValidator()
        {
            RuleFor(e => e.FormId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new FormDtoValidator());
        }
    }
}
