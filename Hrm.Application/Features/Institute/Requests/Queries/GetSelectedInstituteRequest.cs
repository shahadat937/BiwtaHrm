using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Institute.Requests.Queries
{
    public class GetSelectedInstituteRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      