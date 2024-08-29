using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Commands
{
    public class DeleteRewardPunishmentPriorityCommand : IRequest<BaseCommandResponse>
    {
        public int RewardPunishmentPriorityId { get; set; }
    }
}
