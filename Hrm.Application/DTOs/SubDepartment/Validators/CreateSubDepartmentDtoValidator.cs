using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubDepartment.Validators
{
    public class CreateSubDepartmentDtoValidator :AbstractValidator<CreateSubDepartmentDto>
    {
        public CreateSubDepartmentDtoValidator()
        {
            Include(new ISubDepartmentDtoValidator());
        }
    }
}
