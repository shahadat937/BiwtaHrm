using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Division.Requests.Queries
{
    public class GetSelectedDivisionRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      