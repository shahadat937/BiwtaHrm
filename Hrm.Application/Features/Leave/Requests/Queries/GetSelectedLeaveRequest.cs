using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Leaves.Requests.Queries
{
    public class GetSelectedLeaveRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      