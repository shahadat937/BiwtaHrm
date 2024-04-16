using FluentValidation;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.SubBranch.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.SubBranch.ValidatorsSubBranch
{
    public class UpdateSubBranchDtoValidator : AbstractValidator<SubBranchDto>
    {
        public UpdateSubBranchDtoValidator()
        { 
            Include(new ISubBranchDtoValidator());
            RuleFor(x=>x.SubBranchId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
