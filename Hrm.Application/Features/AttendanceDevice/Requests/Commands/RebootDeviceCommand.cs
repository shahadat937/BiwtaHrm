using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Commands
{
    public class RebootDeviceCommand : IRequest<BaseCommandResponse>
    {
        public int DeviceId { get; set; }
    }
}
