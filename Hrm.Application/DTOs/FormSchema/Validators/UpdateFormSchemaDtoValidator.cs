using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSchema.Validators
{
    public class UpdateFormSchemaDtoValidator: AbstractValidator<FormSchemaDto>
    {
        public UpdateFormSchemaDtoValidator()
        {
            RuleFor(x => x.SchemaId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new IFormSchemaDtoValidator());
        }
    }
}
