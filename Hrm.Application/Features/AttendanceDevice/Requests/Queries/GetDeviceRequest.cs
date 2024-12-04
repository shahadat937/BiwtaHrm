using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Application.DTOs.AttDevice;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Queries
{
    public class GetDeviceRequest : IRequest<List<AttDevicesDto>>
    {

    }
}
