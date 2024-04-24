using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubDepartment.Validators
{
    public class ISubDepartmentDtoValidator :AbstractValidator<ISubDepartmentDto>
    {
        public ISubDepartmentDtoValidator()
        {
            RuleFor(b => b.SubDepartmentName)
                .NotEmpty().WithMessage("{PropertyName} is requered.}").MaximumLength(150).WithMessage("{PropertyName} must not exced{ComparisonValue} Characters.");

            RuleFor(p => p.DepartmentId)
                    .NotEmpty().WithMessage("{PropertyName} is required.")
                    .NotNull()
                    .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.");
        }
    }
}
