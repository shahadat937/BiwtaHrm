using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Hrm.Application.DTOs.FormGroup.Validators
{
    public class CreateFormGroupDtoValidator: AbstractValidator<CreateFormGroupDto>
    {
        public CreateFormGroupDtoValidator()
        {
            Include(new IFormGroupDtoValidator());
            RuleFor(x => x.IsActive).NotNull().WithMessage("{PropertyName} is required");
        }
    }
}
