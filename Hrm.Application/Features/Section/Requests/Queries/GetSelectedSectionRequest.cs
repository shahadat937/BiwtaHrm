using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Section.Requests.Queries
{
    public class GetSelectedSectionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      