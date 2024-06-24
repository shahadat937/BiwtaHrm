using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubDepartment.Validators
{
    public class UpdateSubDepartmentDtoValidators:AbstractValidator<SubDepartmentDto>
    {
        public UpdateSubDepartmentDtoValidators()
        {
            Include(new ISubDepartmentDtoValidator());
            RuleFor(p => p.DepartmentId).NotNull().WithMessage("{PropertyName} must be present");

            RuleFor(x => x.SubDepartmentId).NotNull().WithMessage("{propertyName } must be present");
        }
    }
}
