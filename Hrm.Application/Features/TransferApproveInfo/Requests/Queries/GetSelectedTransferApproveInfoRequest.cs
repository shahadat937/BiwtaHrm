using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.TransferApproveInfo.Requests.Queries
{
    public class GetSelectedTransferApproveInfoRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      