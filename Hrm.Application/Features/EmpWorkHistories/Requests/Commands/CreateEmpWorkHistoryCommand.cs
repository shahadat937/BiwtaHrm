using Hrm.Application.DTOs.EmpWorkHistory;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpWorkHistories.Requests.Commands
{
    public class CreateEmpWorkHistoryCommand : IRequest<BaseCommandResponse>
    {
        public List<CreateEmpWorkHistoryDto> EmpWorkHistoryDto { get; set; }
    }
}
