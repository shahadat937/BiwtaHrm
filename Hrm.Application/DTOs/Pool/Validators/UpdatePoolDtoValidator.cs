using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Pool.Validators
{
    public class UpdatePoolDtoValidator : AbstractValidator<PoolDto>
    {
        public UpdatePoolDtoValidator()
        {
            Include(new IPoolDtoValidator());

            RuleFor(x => x.PoolId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
