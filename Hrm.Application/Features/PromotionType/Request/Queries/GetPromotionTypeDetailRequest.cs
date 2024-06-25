
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.PromotionType;

namespace Hrm.Application.Features.PromotionTypes.Requests.Queries
{
    public class GetPromotionTypeDetailRequest : IRequest<PromotionTypeDto>
    {
        public int PromotionTypeId { get; set; }
    }
}
