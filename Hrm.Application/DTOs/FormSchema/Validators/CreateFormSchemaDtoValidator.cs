using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSchema.Validators
{
    public class CreateFormSchemaDtoValidator: AbstractValidator<CreateFormSchemaDto>
    {
        public CreateFormSchemaDtoValidator()
        {
            Include(new IFormSchemaDtoValidator());
        }
    }
}
