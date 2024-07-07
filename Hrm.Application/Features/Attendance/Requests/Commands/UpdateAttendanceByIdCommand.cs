using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Commands
{
    public class UpdateAttendanceByIdCommand: IRequest<BaseCommandResponse>
    {
        public CreateAttendanceDto Attendancedto { get; set; }
    }
}
