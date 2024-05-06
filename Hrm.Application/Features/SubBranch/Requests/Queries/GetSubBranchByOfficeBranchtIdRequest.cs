using Hrm.Application.DTOs.SubBranch;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Requests.Queries
{
    public class GetSubBranchByOfficeBranchIdRequest : IRequest<List<SelectedModel>>
    {
        public int OfficeBranchId { get; set; }
    }
}
