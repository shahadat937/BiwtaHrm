using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.PostingOrderInfo.Requests.Queries
{
    public class GetSelectedPostingOrderInfoRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      