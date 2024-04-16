using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Requests.Commands
{
    public class DeleteSubBranchCommand : IRequest<BaseCommandResponse>
    {
        public int SubBranchId { get; set; }
    }
}
