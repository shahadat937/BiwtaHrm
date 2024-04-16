using Hrm.Application.DTOs.Punishment;
using Hrm.Application.DTOs.Punishment;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Punishment.Requests.Commands
{
    public class UpdatePunishmentCommand : IRequest<BaseCommandResponse>
    {
        public PunishmentDto PunishmentDto { get; set; }
    }
}
