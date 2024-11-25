using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.Devicecmd;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Commands
{
    public class DeviceCmdResponseCommand: IRequest<object>
    {
        public string SN { get; set; }
        public DeviceCmdResponse CommandResponse { get; set; }
    }
}
