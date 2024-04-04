using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.Shift.Requests.Queries
{
    public class GetSelectedShiftRequest : IRequest<List<SelectedModel>>
    {
    }
} 
      