using FluentValidation;
using Hrm.Application.DTOs.GradeType.Validators;
using Hrm.Application.DTOs.GradeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Grade.Validators
{
    public class UpdateGradeDtoValidator : AbstractValidator<GradeDto>
    {
        public UpdateGradeDtoValidator()
        {
            Include(new IGradeDtoValidator());
            RuleFor(x => x.GradeId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
