using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.FormSection.Validators
{
    public class IFormSectionDtoValidator : AbstractValidator<IFormSectionDto>
    {
        public IFormSectionDtoValidator()
        {
            RuleFor(x => x.FormSectionId).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SectionNo).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FormId).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.FormSectionName).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
