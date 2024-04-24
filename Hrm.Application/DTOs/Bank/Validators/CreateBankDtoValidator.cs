using FluentValidation;
using Hrm.Application.DTOs.Bank;
using Hrm.Application.DTOs.Bank.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankBank.Validators
{
    public class CreateBankDtoValidator: AbstractValidator<CreateBankDto>
    {
        public CreateBankDtoValidator()
        {
            Include(new IBankDtoValidator());
        }
    }
}
