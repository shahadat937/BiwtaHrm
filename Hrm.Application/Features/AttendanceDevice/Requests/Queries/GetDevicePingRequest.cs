using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Queries
{
    public class GetDevicePingRequest: IRequest<object>
    {
        public string SN { get; set; }
    }
}
