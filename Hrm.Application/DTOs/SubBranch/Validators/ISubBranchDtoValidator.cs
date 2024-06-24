using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubBranch.Validators
{
    public class ISubBranchDtoValidator : AbstractValidator<ISubBranchDto>
    {
        public ISubBranchDtoValidator()
        {
            RuleFor(b=>b.SubBranchName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
