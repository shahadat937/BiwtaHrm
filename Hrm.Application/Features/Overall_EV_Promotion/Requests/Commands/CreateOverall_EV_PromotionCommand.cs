using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands
{
    public class CreateOverall_EV_PromotionCommand : IRequest<BaseCommandResponse>
    {
        public CreateOverall_EV_PromotionDto Overall_EV_PromotionDto { get; set; }
    }
}
