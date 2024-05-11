using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Year.Validators
{
    public class IYearDtoValidator : AbstractValidator<IYearDto>
    {
        public IYearDtoValidator()
        {
            RuleFor(b => b.YearName)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }

}