using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.AttDevice.Validators
{
    public class IAttDevicesDtoValidator: AbstractValidator<IAttDeviceDto>
    {
        public IAttDevicesDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SN).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
