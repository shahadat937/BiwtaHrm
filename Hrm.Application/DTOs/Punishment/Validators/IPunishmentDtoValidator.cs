using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Punishment.Validators
{

    
        public class IPunishmentDtoValidator : AbstractValidator<IPunishmentDto>
        {
            public IPunishmentDtoValidator()
            {
                RuleFor(b => b.PunishmentName)
                    .NotEmpty().WithMessage("{PropertyName} is required.").MaximumLength(150).WithMessage("{PropertyName} must not exceed {ComparisonValue} characters.");
            }
        }
    
}
