using Hrm.Application.DTOs.LeaveRules;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveRules.Requests.Commands
{
    public class CreateLeaveRuleCommand: IRequest<BaseCommandResponse>
    {
        public CreateLeaveRulesDto createleaveRuleDto { get; set; }
    }
}
