using FluentValidation;
using Hrm.Application.DTOs.Bank;
using Hrm.Application.DTOs.Bank.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Bank.ValidatorsBank
{
    public class UpdateBankDtoValidator : AbstractValidator<BankDto>
    {
        public UpdateBankDtoValidator()
        { 
            Include(new IBankDtoValidator());
            RuleFor(x=>x.BankId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
