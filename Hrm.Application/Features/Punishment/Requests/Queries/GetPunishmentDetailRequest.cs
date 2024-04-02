
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Punishment;

namespace Hrm.Application.Features.Punishments.Requests.Queries
{
    public class GetPunishmentDetailRequest : IRequest<PunishmentDto>
    {
        public int PunishmentId { get; set; }
    }
}
