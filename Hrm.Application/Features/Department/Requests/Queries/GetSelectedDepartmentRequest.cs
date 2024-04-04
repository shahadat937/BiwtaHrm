using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Department.Requests.Queries
{
    public class GetSelectedDepartmentRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      