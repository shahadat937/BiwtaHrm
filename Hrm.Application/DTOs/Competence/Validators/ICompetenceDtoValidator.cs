using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Competence.Validators
{
    public class ICompetenceDtoValidator : AbstractValidator<ICompetenceDto>
    {
        public ICompetenceDtoValidator()
        {
            RuleFor(b=>b.CompetenceName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
