using Hrm.Application.DTOs.SubBranch;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Requests.Queries
{
    public class GetSubBranchByIdRequest : IRequest<SubBranchDto>
    {
        public int SubBranchId { get; set; }
    }
}
