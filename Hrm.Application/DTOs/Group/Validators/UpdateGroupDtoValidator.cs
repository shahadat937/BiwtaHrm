using FluentValidation;
using Hrm.Application.DTOs.Group.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Group.ValidatorsGroup
{
    public class UpdateGroupDtoValidator : AbstractValidator<GroupDto>
    {
        public UpdateGroupDtoValidator()
        {
            Include(new IGroupDtoValidator());

            RuleFor(x => x.GroupId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
