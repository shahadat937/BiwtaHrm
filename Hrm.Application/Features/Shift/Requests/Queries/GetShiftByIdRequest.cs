using Hrm.Application.DTOs.Shift;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Shift.Requests.Queries
{
    public class GetShiftByIdRequest : IRequest<ShiftDto>
    {
        public int ShiftId { get; set; }
    }
}
