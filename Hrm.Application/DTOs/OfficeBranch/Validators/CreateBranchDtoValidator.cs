using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Branch.Validators
{
    public class CreateBranchDtoValidator : AbstractValidator<CreateBranchDto>
    {
        public CreateBranchDtoValidator()
        {
            Include(new IBranchDtoValidator());
        }
    }
}
