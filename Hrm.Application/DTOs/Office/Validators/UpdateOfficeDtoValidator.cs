using FluentValidation;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.Office.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Office.ValidatorsOffice
{
    public class UpdateOfficeDtoValidator : AbstractValidator<OfficeDto>
    {
        public UpdateOfficeDtoValidator()
        { 
            Include(new IOfficeDtoValidator());
            RuleFor(x=>x.OfficeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
