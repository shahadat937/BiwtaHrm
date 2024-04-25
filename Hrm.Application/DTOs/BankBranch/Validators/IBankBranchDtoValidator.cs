using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankBranch.Validators
{

    
        public class IBankBranchDtoValidator : AbstractValidator<IBankBranchDto>
        {
            public IBankBranchDtoValidator()
            {
                RuleFor(b => b.BankBranchName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
