using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.ExamType.Requests.Queries
{
    public class GetSelectedExamTypeRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      