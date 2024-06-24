using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Designation.Validators
{
    public class CreateDesignationDtoValidator : AbstractValidator<CreateDesignationDto>
    {
        public CreateDesignationDtoValidator()
        {
            Include(new IDesignationDtoValidator());
        }
    }
}
