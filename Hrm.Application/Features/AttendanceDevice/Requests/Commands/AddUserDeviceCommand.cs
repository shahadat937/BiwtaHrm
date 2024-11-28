using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.AttDevice;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Commands
{
    public class AddUserDeviceCommand : IRequest<BaseCommandResponse>
    {
        public AddUserDeviceDto AddUserDeviceDto { get; set; }
    }
}
