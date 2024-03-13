using FluentValidation;
using Hrm.Application.DTOs.District;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Country.Validators
{
    public class ICountryDtoValidator : AbstractValidator<ICountryDto>
    {
        public ICountryDtoValidator()
        {
            RuleFor(b => b.CountryName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");
        }
    }
}
