using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Punishment.Validators
{
    public class UpdatePunishmentDtoValidator : AbstractValidator<PunishmentDto>
    {
        public UpdatePunishmentDtoValidator()
        {
            Include(new IPunishmentDtoValidator());

            RuleFor(x => x.PunishmentId).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}
