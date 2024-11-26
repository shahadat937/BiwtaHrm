using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.PendingDevice;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Queries
{
    public class GetPendingDevicesRequest : IRequest<List<PendingDeviceDto>>
    {
        
    }
}
