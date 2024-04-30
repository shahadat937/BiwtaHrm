using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.EmpTnsferPostingJoin.Validators
{
    public class CreateEmpTnsferPostingJoinDtoValidator : AbstractValidator<CreateEmpTnsferPostingJoinDto>
    {
        public CreateEmpTnsferPostingJoinDtoValidator()
        {
            Include(new IEmpTnsferPostingJoinDtoValidator());
        }
    }
}
