using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Requests.Commands
{
    public class CreateSubBranchCommand : IRequest<BaseCommandResponse>
    {
        public CreateSubBranchDto SubBranchDto { get; set; }
    }
}
