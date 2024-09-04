using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Section.Validators
{
    public class ISectionDtoValidator : AbstractValidator<ISectionDto>
    {
        public ISectionDtoValidator()
        {
            RuleFor(b=>b.SectionName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
