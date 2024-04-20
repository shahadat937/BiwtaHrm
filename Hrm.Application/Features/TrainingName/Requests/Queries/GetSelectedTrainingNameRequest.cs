using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.TrainingName.Requests.Queries
{
    public class GetSelectedTrainingNameRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      