using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Office.Validators
{
    public class IOfficeDtoValidator : AbstractValidator<IOfficeDto>
    {
        public IOfficeDtoValidator()
        {
            RuleFor(b=>b.OfficeName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
