using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Result.Validators
{
    public class UpdateResultDtoValidator : AbstractValidator<ResultDto>
    {
        public UpdateResultDtoValidator()
        {
            Include(new IResultDtoValidator());

            RuleFor(x => x.ResultId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
