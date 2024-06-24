using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.SubDepartment.Requests.Queries
{
    public class GetSelectedSubDepartmentRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      