using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.BloodGroups.Requests.Queries
{
    public class GetSelectedBloodGroupRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      