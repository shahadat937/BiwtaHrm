using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.AttDevice.Validators
{
    public class CreateAttDeviceDtoValidator: AbstractValidator<CreateAttDeviceDto>
    {
        public CreateAttDeviceDtoValidator()
        {
            Include(new IAttDevicesDtoValidator());
            RuleFor(x => x.AccDevices).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Timezone).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
