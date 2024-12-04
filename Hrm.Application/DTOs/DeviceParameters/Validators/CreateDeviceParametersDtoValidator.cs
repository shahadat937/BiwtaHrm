using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;

namespace Hrm.Application.DTOs.DeviceParameters.Validators
{
    public class CreateDeviceParametersDtoValidator: AbstractValidator<CreateDeviceParametersDto>
    {
        public CreateDeviceParametersDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} should not be empty");
            RuleFor(x => x.Value).NotEmpty().WithMessage("{PropertyName} should not be empty");
        }
    }
}
