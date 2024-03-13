using FluentValidation;
using Hrm.Application.DTOs.Department;
using Hrm.Application.DTOs.Department.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Department.ValidatorsDepartment
{
    public class UpdateDepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public UpdateDepartmentDtoValidator()
        { 
            Include(new IDepartmentDtoValidator());
            RuleFor(x=>x.DepartmentId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
