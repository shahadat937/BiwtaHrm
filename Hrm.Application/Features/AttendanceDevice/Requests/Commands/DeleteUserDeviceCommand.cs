﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Commands
{
    public class DeleteUserDeviceCommand : IRequest<BaseCommandResponse>
    {
        public int EmpId { get; set; }
        public int DeviceId { get; set; }
    }
}
