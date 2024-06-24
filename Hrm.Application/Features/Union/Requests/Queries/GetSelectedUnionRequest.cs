using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Union.Requests.Queries
{
    public class GetSelectedUnionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      