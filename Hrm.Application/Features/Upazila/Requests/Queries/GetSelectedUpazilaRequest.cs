using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Upazila.Requests.Queries
{
    public class GetSelectedUpazilaRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      