using Hrm.Application.DTOs.Workday;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Workday.Requests.Commands
{
    public class UpdateWorkdayCommand : IRequest<BaseCommandResponse>
    {
        public WorkdayDto WorkdayDto { get; set; }
    }
}
