using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.BankAccountType.Validators
{
    public class IBankAccountTypeDtoValidator : AbstractValidator<IBankAccountTypeDto>
    {
        public IBankAccountTypeDtoValidator()
        {
            RuleFor(b=>b.BankAccountTypeName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }
}
