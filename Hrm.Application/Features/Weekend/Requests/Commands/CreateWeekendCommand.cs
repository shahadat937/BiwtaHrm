using Hrm.Application.DTOs.Weekend;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Weekend.Requests.Commands
{
    public class CreateEmployeeCommand : IRequest<BaseCommandResponse>
    {
        public CreateWeekendDto WeekendDto { get; set; }
    }
}
