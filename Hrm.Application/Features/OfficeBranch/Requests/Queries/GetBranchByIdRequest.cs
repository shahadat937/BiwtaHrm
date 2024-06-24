using Hrm.Application.DTOs.Branch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Requests.Queries
{
    public class GetBranchByIdRequest : IRequest<BranchDto>
    {
        public int BranchId { get; set; }
    }
}
