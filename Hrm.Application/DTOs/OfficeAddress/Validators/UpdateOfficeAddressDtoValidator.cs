using FluentValidation;
using Hrm.Application.DTOs.OfficeAddress;
using Hrm.Application.DTOs.OfficeAddress.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeAddress.ValidatorsOfficeAddress
{
    public class UpdateOfficeAddressDtoValidator : AbstractValidator<OfficeAddressDto>
    {
        public UpdateOfficeAddressDtoValidator()
        { 
            Include(new IOfficeAddressDtoValidator());
            RuleFor(x=>x.OfficeAddressId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
