using FluentValidation;
using Hrm.Application.DTOs.BloodGroup.Validators;
using Hrm.Application.DTOs.BloodGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Year.Validators
{
    public class UpdateYearDtoValidator : AbstractValidator<YearDto>
    {
        public UpdateYearDtoValidator()
        {
            Include(new IYearDtoValidator());

            RuleFor(x => x.YearId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}