using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Branch.Validators
{
    public class UpdateBranchDtoValidator : AbstractValidator<BranchDto>
    {
        public UpdateBranchDtoValidator()
        {
            Include(new IBranchDtoValidator());

            RuleFor(x => x.BranchName).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
