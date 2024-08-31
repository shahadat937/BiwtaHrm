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
    public class CreateRewardPunishmentPriorityCommand : IRequest<BaseCommandResponse>
    {
        public CreateRewardPunishmentPriorityDto RewardPunishmentPriorityDto { get; set; }
    }
}
