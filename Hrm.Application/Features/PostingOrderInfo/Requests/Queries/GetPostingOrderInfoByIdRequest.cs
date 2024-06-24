using Hrm.Application.DTOs.PostingOrderInfo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Requests.Queries
{
    public class GetPostingOrderInfoByIdRequest : IRequest<PostingOrderInfoDto>
    {
        public int PostingOrderInfoId { get; set; }
    }
}
