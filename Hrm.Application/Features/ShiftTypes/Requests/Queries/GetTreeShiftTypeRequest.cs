using Hrm.Application.DTOs.ShiftType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ShiftTypes.Requests.Queries
{
    public class GetTreeShiftTypeRequest : IRequest<List<TreeShiftTypeDto>>
    {
    }
}
