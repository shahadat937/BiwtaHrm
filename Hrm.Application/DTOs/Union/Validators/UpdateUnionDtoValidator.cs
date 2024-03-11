using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Union.Validators
{
    public class UpdateUnionDtoValidator : AbstractValidator<UnionDto>
    {
        public UpdateUnionDtoValidator()
        {
            Include(new IUnionDtoValidators());

            RuleFor(x => x.UnionId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
