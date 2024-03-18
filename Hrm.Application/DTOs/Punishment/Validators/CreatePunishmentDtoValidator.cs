using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.DTOs.Punishment.Validators
{
    public class CreatePunishmentDtoValidator : AbstractValidator<CreatePunishmentDto>
    {
        public CreatePunishmentDtoValidator()
        {
            Include(new IPunishmentDtoValidator());
        }
    }
}
