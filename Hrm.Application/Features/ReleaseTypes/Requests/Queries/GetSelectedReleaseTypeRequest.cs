using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.ReleaseTypes.Requests.Queries
{
    public class GetSelectedReleaseTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      