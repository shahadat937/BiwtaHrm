using FluentValidation;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Department.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.DepartmentDepartment.Validators
{
    public class CreateDepartmentDtoValidator: AbstractValidator<CreateDepartmentDto>
    {
        public CreateDepartmentDtoValidator()
        {
            Include(new IDepartmentDtoValidator());
        }
    }
}
