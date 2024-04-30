using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Requests.Commands
{
    public class CreatePostingOrderInfoCommand : IRequest<BaseCommandResponse>
    {
        public CreatePostingOrderInfoDto PostingOrderInfoDto { get; set; }
    }
}
