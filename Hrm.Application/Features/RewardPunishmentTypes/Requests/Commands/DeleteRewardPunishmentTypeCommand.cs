using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands
{
    public class DeleteRewardPunishmentTypeCommand : IRequest<BaseCommandResponse>
    {
        public int RewardPunishmentTypeId { get; set; }
    }
}
