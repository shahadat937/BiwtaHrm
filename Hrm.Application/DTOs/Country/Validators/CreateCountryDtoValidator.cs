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
    public class CreateCountryDtoValidator : AbstractValidator<CreateCountryDto>
    {
        public CreateCountryDtoValidator()
        {
            Include(new ICountryDtoValidator());
        }
    }
}

