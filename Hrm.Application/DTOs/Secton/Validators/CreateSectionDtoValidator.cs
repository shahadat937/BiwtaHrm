using FluentValidation;
using Hrm.Application.DTOs.Section;
using Hrm.Application.DTOs.Section.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SectionSection.Validators
{
    public class CreateSectionDtoValidator: AbstractValidator<CreateSectionDto>
    {
        public CreateSectionDtoValidator()
        {
            Include(new ISectionDtoValidator());
        }
    }
}
