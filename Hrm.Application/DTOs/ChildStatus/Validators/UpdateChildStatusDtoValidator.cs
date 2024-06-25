using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.ChildStatus.Validators
{
    public class UpdateChildStatusDtoValidator : AbstractValidator<ChildStatusDto>
    {
        public UpdateChildStatusDtoValidator()
        {
            Include(new IChildStatusDtoValidator());

            RuleFor(x => x.ChildStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
