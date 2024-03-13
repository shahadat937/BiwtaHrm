using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Department.Validators
{
    public class IDepartmentDtoValidator : AbstractValidator<IDepartmentDto>
    {
        public IDepartmentDtoValidator()
        {
            RuleFor(b=>b.DepartmentName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
