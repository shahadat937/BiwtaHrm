using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Reward;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reward.Requests.Commands
{
    public class CreateRewardCommand : IRequest<BaseCommandResponse>
    {
        public CreateRewardDto RewardDto { get; set; }
    }
}
