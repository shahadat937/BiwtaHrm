using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.DepReleaseInfo.Requests.Queries
{
    public class GetSelectedDepReleaseInfoRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      