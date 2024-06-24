using FluentValidation;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.SubBranch.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubBranchSubBranch.Validators
{
    public class CreateSubBranchDtoValidator: AbstractValidator<CreateSubBranchDto>
    {
        public CreateSubBranchDtoValidator()
        {
            Include(new ISubBranchDtoValidator());
        }
    }
}
