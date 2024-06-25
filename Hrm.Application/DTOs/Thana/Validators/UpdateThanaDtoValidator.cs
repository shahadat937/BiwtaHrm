using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Thana.Validators
{
    public class UpdateThanaDtoValidator : AbstractValidator<ThanaDto>
    {
        public UpdateThanaDtoValidator()
        {
            Include(new IThanaDtoValidators());

            RuleFor(x => x.ThanaId).NotNull().WithMessage("{PropertyName} must be present");
        }   
    }
}
