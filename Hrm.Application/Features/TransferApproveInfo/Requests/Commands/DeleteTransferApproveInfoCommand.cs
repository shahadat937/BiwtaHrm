using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Requests.Commands
{
    public class DeleteTransferApproveInfoCommand : IRequest<BaseCommandResponse>
    {
        public int TransferApproveInfoId { get; set; }
    }
}
