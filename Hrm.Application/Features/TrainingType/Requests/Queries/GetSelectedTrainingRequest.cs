using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.TrainingType.Requests.Queries
{
    public class GetSelectedTrainingTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      