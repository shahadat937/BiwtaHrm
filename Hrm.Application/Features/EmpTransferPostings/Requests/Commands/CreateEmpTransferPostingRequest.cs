using Hrm.Application.DTOs.EmpTransferPosting;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTransferPostings.Requests.Commands
{
    public class CreateEmpTransferPostingRequest : IRequest<BaseCommandResponse>
    {
        public CreateEmpTransferPostingDto EmpTransferPostingDto { get; set; }
    }
}
