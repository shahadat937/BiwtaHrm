
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.RewardPunishmentType;

namespace Hrm.Application.Features.RewardPunishmentTypes.Requests.Queries
{
    public class GetRewardPunishmentTypeDetailRequest : IRequest<RewardPunishmentTypeDto>
    {
        public int RewardPunishmentTypeId { get; set; }
    }
}
