
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.RewardPunishmentPriority;

namespace Hrm.Application.Features.RewardPunishmentPrioritys.Requests.Queries
{
    public class GetRewardPunishmentPriorityDetailRequest : IRequest<RewardPunishmentPriorityDto>
    {
        public int RewardPunishmentPriorityId { get; set; }
    }
}
