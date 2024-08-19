using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormField.Validators
{
    public class CreateFormFieldDtoValidator: AbstractValidator<CreateFormFieldDto>
    {
        public CreateFormFieldDtoValidator()
        {
            Include(new IFormFieldDtoValidator());
        }
    }
}
