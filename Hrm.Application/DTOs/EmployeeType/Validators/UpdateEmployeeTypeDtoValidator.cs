using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmployeeType.Validators
{
    public class UpdateEmployeeTypeDtoValidator : AbstractValidator<EmployeeTypeDto>
    {
        public UpdateEmployeeTypeDtoValidator()
        {
            Include(new IEmployeeTypeDtoValidator());

            RuleFor(x => x.EmployeeTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
