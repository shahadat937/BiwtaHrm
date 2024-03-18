using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Group.Validators
{
    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupDto>
    {
        public CreateGroupDtoValidator()
        {
            Include(new IGroupDtoValidator());
        }
    }
}
