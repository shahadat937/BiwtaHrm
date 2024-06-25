using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Branch.Validators
{

    
        public class IBranchDtoValidator : AbstractValidator<IBranchDto>
        {
            public IBranchDtoValidator()
            {
                RuleFor(b => b.BranchName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
