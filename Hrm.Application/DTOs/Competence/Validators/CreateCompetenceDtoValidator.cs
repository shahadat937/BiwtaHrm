using FluentValidation;
using Hrm.Application.DTOs.Competence;
using Hrm.Application.DTOs.Competence.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.CompetenceCompetence.Validators
{
    public class CreateCompetenceDtoValidator: AbstractValidator<CreateCompetenceDto>
    {
        public CreateCompetenceDtoValidator()
        {
            Include(new ICompetenceDtoValidator());
        }
    }
}
