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
    public class CreatePromotionTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreatePromotionTypeDto PromotionTypeDto { get; set; }
    }
}
