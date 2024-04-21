using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeAddress.Validators
{
    public class IOfficeAddressDtoValidator : AbstractValidator<IOfficeAddressDto>
    {
        public IOfficeAddressDtoValidator()
        {
            RuleFor(b=>b.OfficeAddressName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
