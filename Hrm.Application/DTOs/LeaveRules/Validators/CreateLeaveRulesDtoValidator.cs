using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRules.Validators
{
    public class CreateLeaveRulesDtoValidator: AbstractValidator<CreateLeaveRulesDto>
    {
        public CreateLeaveRulesDtoValidator()
        {
            Include(new ILeaveRulesDtoValidator());
            RuleFor(e => e.LeaveTypeID).NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
}
