using FluentValidation;
using Hrm.Application.DTOs.MaritalStatus.Validators;
using Hrm.Application.DTOs.MaritalStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators
{
    public class UpdateEmpTnsferPostingJoinDtoValidators : AbstractValidator<EmpTnsferPostingJoinDto>
    {
        public UpdateEmpTnsferPostingJoinDtoValidators()
        {
            Include(new IEmpTnsferPostingJoinDtoValidator());

            RuleFor(x => x.EmpTnsferPostingJoinId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
