using Hrm.Application.DTOs.LeaveRules;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Requests.Queries
{
    public class GetLeaveRulesByLeaveTypeIdRequest:IRequest<List<LeaveRulesDto>>
    {
        public int LeaveTypeId { get; set; }
    }
}
