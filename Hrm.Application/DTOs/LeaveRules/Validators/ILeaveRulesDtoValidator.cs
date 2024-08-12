using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRules.Validators
{
    public class ILeaveRulesDtoValidator: AbstractValidator<ILeaveRulesDto>
    {
        public ILeaveRulesDtoValidator() {
            RuleFor(e => e.RuleName).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.RuleValue).NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(e => e.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
