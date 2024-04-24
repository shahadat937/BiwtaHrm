using FluentValidation;
using Hrm.Application.DTOs.Competence;
using Hrm.Application.DTOs.Competence.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Competence.ValidatorsCompetence
{
    public class UpdateCompetenceDtoValidator : AbstractValidator<CompetenceDto>
    {
        public UpdateCompetenceDtoValidator()
        { 
            Include(new ICompetenceDtoValidator());
            RuleFor(x=>x.CompetenceId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
