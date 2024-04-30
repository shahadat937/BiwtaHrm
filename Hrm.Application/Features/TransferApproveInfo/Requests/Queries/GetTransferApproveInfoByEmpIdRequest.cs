using Hrm.Application.DTOs.TransferApproveInfo;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Requests.Queries
{
    public class GetTransferApproveInfoByEmployeeIdRequest:IRequest<List<SelectedModel>>
    {
        public int EmpId { get; set; }
    }
}
