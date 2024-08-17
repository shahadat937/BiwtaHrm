using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Form.Validators
{
    public class IFormDtoValidator: AbstractValidator<IFormDto>
    {
        public IFormDtoValidator() {
            RuleFor(x => x.FormName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
