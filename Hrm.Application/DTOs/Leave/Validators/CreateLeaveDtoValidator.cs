using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Leave.Validators
{
    public class CreateLeaveDtoValidator : AbstractValidator<CreateLeaveDto>
    {
        public CreateLeaveDtoValidator()
        {
            Include(new ILeaveDtoValidator());
        }
    }
}
