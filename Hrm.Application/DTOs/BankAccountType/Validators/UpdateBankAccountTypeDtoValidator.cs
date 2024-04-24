using FluentValidation;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.DTOs.BankAccountType.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankAccountType.ValidatorsBankAccountType
{
    public class UpdateBankAccountTypeDtoValidator : AbstractValidator<BankAccountTypeDto>
    {
        public UpdateBankAccountTypeDtoValidator()
        { 
            Include(new IBankAccountTypeDtoValidator());
            RuleFor(x=>x.BankAccountTypeId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
