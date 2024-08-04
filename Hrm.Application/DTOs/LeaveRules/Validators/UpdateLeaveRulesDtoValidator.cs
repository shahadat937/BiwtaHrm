using FluentValidation;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.LeaveRules.Validators
{
    public class UpdateLeaveRulesDtoValidator: AbstractValidator<CreateLeaveRulesDto>
    {
        public UpdateLeaveRulesDtoValidator()
        {
            RuleFor(e => e.RuleId).NotEmpty().WithMessage("{PropertyName} is required");
            Include(new CreateLeaveRulesDtoValidator());
        }
    }
}
