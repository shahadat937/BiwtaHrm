using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankBranch.Validators
{
    public class UpdateBankBranchDtoValidators : AbstractValidator<BankBranchDto>
    {
        public UpdateBankBranchDtoValidators()
        {
            Include(new IBankBranchDtoValidator());

            RuleFor(x => x.BankBranchId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
