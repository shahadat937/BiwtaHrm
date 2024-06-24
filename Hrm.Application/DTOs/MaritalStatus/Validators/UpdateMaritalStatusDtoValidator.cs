using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.MaritalStatus.Validators
{
    public class UpdateMaritalStatusDtoValidator : AbstractValidator<MaritalStatusDto>
    {
        public UpdateMaritalStatusDtoValidator()
        {
            Include(new IMaritalStatusDtoValidators());

            RuleFor(x => x.MaritalStatusId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
