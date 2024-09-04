using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.RewardPunishmentPriority;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands
{
    public class UpdateRewardPunishmentPriorityCommand : IRequest<BaseCommandResponse>
    {
        public RewardPunishmentPriorityDto RewardPunishmentPriorityDto { get; set; }
    }
}
