using FluentValidation;
using Hrm.Application.DTOs.BankBranch;
using Hrm.Application.DTOs.BankBranch.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankBranchBankBranch.Validators
{
    public class CreateBankBranchDtoValidator: AbstractValidator<CreateBankBranchDto>
    {
        public CreateBankBranchDtoValidator()
        {
            Include(new IBankBranchDtoValidator());
        }
    }
}
