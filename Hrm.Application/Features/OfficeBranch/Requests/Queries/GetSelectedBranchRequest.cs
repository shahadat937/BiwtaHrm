using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Branch.Requests.Queries
{
    public class GetSelectedBranchRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      