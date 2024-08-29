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
    public class CreateRewardPunishmentTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateRewardPunishmentTypeDto RewardPunishmentTypeDto { get; set; }
    }
}
