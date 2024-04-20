using FluentValidation;
using Hrm.Application.DTOs.BankAccountType;
using Hrm.Application.DTOs.BankAccountType.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankAccountTypeBankAccountType.Validators
{
    public class CreateBankAccountTypeDtoValidator: AbstractValidator<CreateBankAccountTypeDto>
    {
        public CreateBankAccountTypeDtoValidator()
        {
            Include(new IBankAccountTypeDtoValidator());
        }
    }
}
