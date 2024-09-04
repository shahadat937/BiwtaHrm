using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.RewardPunishmentType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RewardPunishmentTypes.Requests.Commands
{
    public class UpdateRewardPunishmentTypeCommand : IRequest<BaseCommandResponse>
    {
        public RewardPunishmentTypeDto RewardPunishmentTypeDto { get; set; }
    }
}
