using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators
{


    public class IEmpTnsferPostingJoinDtoValidator : AbstractValidator<IEmpTnsferPostingJoinDto>
    {
        public IEmpTnsferPostingJoinDtoValidator()
        {
            RuleFor(b => b.EmpTnsferPostingJoinName)
                .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
        }
    }

}
