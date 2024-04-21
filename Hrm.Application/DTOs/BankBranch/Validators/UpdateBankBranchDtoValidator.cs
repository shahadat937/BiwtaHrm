using FluentValidation;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.BankBranch.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankBranch.ValidatorsBankBranch
{
    public class UpdateBankBranchDtoValidator : AbstractValidator<BankBranchDto>
    {
        public UpdateBankBranchDtoValidator()
        { 
            Include(new IBankBranchDtoValidator());
            RuleFor(x=>x.BankBranchId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
