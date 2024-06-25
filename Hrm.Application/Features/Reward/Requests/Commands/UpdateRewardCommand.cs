using Hrm.Application.DTOs.Reward;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reward.Requests.Commands
{
    public class UpdateRewardCommand : IRequest<Unit>
    {
        public RewardDto RewardDto { get; set; }
    }
}
