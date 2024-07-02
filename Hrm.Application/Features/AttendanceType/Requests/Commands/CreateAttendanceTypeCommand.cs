using Hrm.Application.DTOs.AttendanceType;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.AttendanceType.Requests.Commands
{
    public class CreateAttendanceTypeCommand:IRequest<BaseCommandResponse>
    {
        public CreateAttendanceTypeDto AttendanceTypeDto { get; set; }
    }
}
