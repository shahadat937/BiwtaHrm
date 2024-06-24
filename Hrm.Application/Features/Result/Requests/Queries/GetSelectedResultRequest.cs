using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Results.Requests.Queries
{
    public class GetSelectedResultRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      