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
    public class UpdatePostingOrderInfoCommand : IRequest<Unit>
    {
        public PostingOrderInfoDto PostingOrderInfoDto { get; set; }
    }
}
