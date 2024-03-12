using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionType.Request.Commands
{
    public class DeletePromotionTypeCommand : IRequest<BaseCommandResponse>
    {
        public int PromotionTypeId { get; set; }
    }
}
