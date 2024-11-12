using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRequest.Requests.Queries
{
    public class GetAllLeaveTypeAmountByEmpIdRequest: IRequest<object>
    {
        public int EmpId { get; set; }
        public DateTime? LeaveStartDate {  get; set; }
        public DateTime? LeaveEndDate { get; set; }
    }
}
