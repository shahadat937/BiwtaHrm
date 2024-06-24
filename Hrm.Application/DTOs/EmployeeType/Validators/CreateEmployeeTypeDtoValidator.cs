using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmployeeType.Validators
{
    public class CreateEmployeeTypeDtoValidator : AbstractValidator<CreateEmployeeTypeDto>
    {
        public CreateEmployeeTypeDtoValidator()
        {
            Include(new IEmployeeTypeDtoValidator());
        }
    }
}
