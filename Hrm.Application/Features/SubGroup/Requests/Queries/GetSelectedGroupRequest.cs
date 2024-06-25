using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Group.Requests.Queries
{
    public class GetSelectedGroupRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      