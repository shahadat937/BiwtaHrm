using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hrm.Shared.Models;
using MediatR;

namespace Hrm.Application.Features.AttendanceDevice.Requests.Queries
{
    public class GetSelectedDeviceRequest : IRequest<List<SelectedModel>>
    {

    }
}
