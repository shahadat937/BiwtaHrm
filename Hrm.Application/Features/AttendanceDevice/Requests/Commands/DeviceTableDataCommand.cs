using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Commands
{
    public class DeviceTableDataCommand : IRequest<object>
    {
        public string SN { get; set; }
        public string Table { get; set; }
        public string? RequestBody { get; set; }
    }
}
