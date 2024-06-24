using FluentValidation;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.DTOs.OfficeAddress.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeAddress.Validators
{
    public class CreateOfficeAddressDtoValidator: AbstractValidator<CreateOfficeAddressDto>
    {
        public CreateOfficeAddressDtoValidator()
        {
            Include(new IOfficeAddressDtoValidator());
        }
    }
}
