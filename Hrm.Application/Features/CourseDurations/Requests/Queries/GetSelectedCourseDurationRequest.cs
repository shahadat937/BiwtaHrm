using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.CourseDurations.Requests.Queries
{
    public class GetSelectedCourseDurationRequest : IRequest<List<SelectedModel>>
    {

    }
} 
      