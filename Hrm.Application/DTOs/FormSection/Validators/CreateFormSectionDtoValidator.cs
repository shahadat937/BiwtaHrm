using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.FormSection.Validators
{
    public class CreateFormSectionDtoValidator: AbstractValidator<CreateFormSectionDto>
    {
        public CreateFormSectionDtoValidator()
        {
            Include(new IFormSectionDtoValidator());
            RuleFor(x => x.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
