using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.AttDevice.Validators
{
    public class AttDevicesDtoValidator : AbstractValidator<AttDevicesDto>
    {
        public AttDevicesDtoValidator()
        {
            Include(new IAttDevicesDtoValidator());
            RuleFor(x => x.Timezone).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
