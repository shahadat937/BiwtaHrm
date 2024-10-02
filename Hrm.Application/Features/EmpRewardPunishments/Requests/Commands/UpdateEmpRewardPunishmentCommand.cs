using Hrm.Application.DTOs.EmpRewardPunishment;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpRewardPunishments.Requests.Commands
{
    public class UpdateEmpRewardPunishmentCommand : IRequest<BaseCommandResponse>
    {
        public CreateEmpRewardPunishmentDto EmpRewardPunishmentDto { get; set; }
    }
}
