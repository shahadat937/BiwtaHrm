using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Designations.Requests.Queries
{
    public class GetSelectedDesignationRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      