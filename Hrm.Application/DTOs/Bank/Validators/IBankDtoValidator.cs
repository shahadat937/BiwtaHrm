using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Bank.Validators
{
    public class IBankDtoValidator : AbstractValidator<IBankDto>
    {
        public IBankDtoValidator()
        {
            RuleFor(b=>b.BankName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
