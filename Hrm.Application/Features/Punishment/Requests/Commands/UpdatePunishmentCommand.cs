using Hrm.Application.DTOs.Punishment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Punishment.Requests.Commands
{
    public class UpdatePunishmentCommand : IRequest<Unit>
    {
        public PunishmentDto PunishmentDto { get; set; }
    }
}
