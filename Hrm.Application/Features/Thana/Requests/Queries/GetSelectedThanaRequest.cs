using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Thana.Requests.Queries
{
    public class GetSelectedThanaRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      