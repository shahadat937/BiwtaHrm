using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Ward.Requests.Queries
{
    public class GetSelectedWardRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      