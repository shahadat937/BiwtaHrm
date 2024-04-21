using FluentValidation;
using Hrm.Application.DTOs.Office;
using Hrm.Application.DTOs.Office.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.OfficeOffice.Validators
{
    public class CreateOfficeDtoValidator: AbstractValidator<CreateOfficeDto>
    {
        public CreateOfficeDtoValidator()
        {
            Include(new IOfficeDtoValidator());
        }
    }
}
