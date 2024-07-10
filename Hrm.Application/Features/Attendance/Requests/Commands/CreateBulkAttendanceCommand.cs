using Hrm.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Commands
{
    public class CreateBulkAttendanceCommand:IRequest<BaseCommandResponse>
    {
        public IFormFile csvFile;
    }
}
