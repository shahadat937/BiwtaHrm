
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.Overall_EV_Promotion;

namespace Hrm.Application.Features.Overall_EV_Promotions.Requests.Queries
{
    public class GetOverall_EV_PromotionDetailRequest : IRequest<Overall_EV_PromotionDto>
    {
        public int Overall_EV_PromotionId { get; set; }
    }
}
