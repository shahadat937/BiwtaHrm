using Hrm.Application.DTOs.AttendanceStatus;
using Hrm.Application.DTOs.AttendanceStatus.Validators;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceStatus.Requests.Commands
{
    public class CreateAttendanceStatusCommand:IRequest<BaseCommandResponse>
    {
        public CreateAttendanceStatusDto AttendanceStatusDto { get; set; }
    }
}
