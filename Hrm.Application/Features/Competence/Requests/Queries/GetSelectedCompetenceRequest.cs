using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Competence.Requests.Queries
{
    public class GetSelectedCompetenceRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      