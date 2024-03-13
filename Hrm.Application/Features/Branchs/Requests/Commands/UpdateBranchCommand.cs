using Hrm.Application.DTOs.Branch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Requests.Commands
{
    public class UpdateBranchCommand : IRequest<Unit>
    {
        public BranchDto BranchDto { get; set; }
    }
}
