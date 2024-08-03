using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveType.Requests.Queries
{
    public class GetLeaveTypeByIdRequest: IRequest<object>
    {
        public int LeaveTypeId { get; set; }
    }
}
