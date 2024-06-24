using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.SubBranch.Requests.Queries
{
    public class GetSelectedSubBranchRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      