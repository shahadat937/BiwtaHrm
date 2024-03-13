using FluentValidation;
using Hrm.Application.DTOs.District.Validators;
using Hrm.Application.DTOs.District;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Country.Validators
{
    public class UpdateCountryDtoValidator : AbstractValidator<CountryDto>
    {
        public UpdateCountryDtoValidator()
        {
            Include(new ICountryDtoValidator());
            RuleFor(x => x.CountrytId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
