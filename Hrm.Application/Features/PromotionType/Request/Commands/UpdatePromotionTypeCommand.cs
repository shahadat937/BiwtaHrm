using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.DTOs.PromotionType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionType.Request.Commands
{
    public class UpdatePromotionTypeCommand : IRequest<BaseCommandResponse>
    {
        public required PromotionTypeDto PromotionTypeDto { get; set; }
    }
}
