using FluentValidation;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.Section.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Section.ValidatorsSection
{
    public class UpdateSectionDtoValidator : AbstractValidator<SectionDto>
    {
        public UpdateSectionDtoValidator()
        { 
            Include(new ISectionDtoValidator());
            RuleFor(x=>x.SectionId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
