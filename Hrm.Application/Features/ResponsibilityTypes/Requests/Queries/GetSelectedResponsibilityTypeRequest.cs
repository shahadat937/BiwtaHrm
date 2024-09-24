using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.ResponsibilityTypes.Requests.Queries
{
    public class GetSelectedResponsibilityTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      