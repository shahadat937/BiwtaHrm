using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.FormSchema.Validators
{
    public class IFormSchemaDtoValidator: AbstractValidator<IFormSchemaDto>
    {
        public IFormSchemaDtoValidator()
        {
            RuleFor(x => x.FormId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FieldId).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.IsActive).NotEmpty().WithMessage("{PropertyName} is required");

        }
    }
}
