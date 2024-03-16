using FluentValidation;
using Hrm.Application.DTOs.Country.Validators;
using Hrm.Application.DTOs.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.GradeType.Validators
{
    public class UpdateGradeDtoValidator : AbstractValidator<GradeTypeDto>
    {
        public UpdateGradeDtoValidator()
        {
            Include(new IGradeTypeDtoValidator());
            RuleFor(x => x.GradeTypeId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
