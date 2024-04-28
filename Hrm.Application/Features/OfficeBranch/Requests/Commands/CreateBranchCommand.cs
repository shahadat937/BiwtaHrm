using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Requests.Commands
{
    public class CreateBranchCommand : IRequest<BaseCommandResponse>
    {
        public CreateBranchDto BranchDto { get; set; }
    }
}
