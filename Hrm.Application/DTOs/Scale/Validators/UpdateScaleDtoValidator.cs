using FluentValidation;
using Hrm.Application.DTOs.Scale.Validators;
using Hrm.Application.DTOs.Scale;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Scale.Validators
{

    public class UpdateScaleDtoValidator : AbstractValidator<ScaleDto>
    {
        public UpdateScaleDtoValidator()
        {
            Include(new IScaleDtoValidator());

            RuleFor(x => x.ScaleId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
