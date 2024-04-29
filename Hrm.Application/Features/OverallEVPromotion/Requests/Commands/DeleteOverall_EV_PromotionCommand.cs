using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotion.Requests.Commands
{
    public class DeleteOverall_EV_PromotionCommand : IRequest<BaseCommandResponse>
    {
        public int Overall_EV_PromotionId { get; set; }
    }
}
