using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormFieldType.Validators
{
    public class CreateFormFieldTypeDtoValidator: AbstractValidator<CreateFormFieldTypeDto>
    {
        public CreateFormFieldTypeDtoValidator()
        {
            Include(new IFormFieldTypeDtoValidator());
        }
    }
}
