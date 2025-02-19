﻿using Hrm.Application.DTOs.Attendance;
using Hrm.Application.Features.Attendance.Handlers.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Attendance.Requests.Queries
{
    public class GetAttendanceRequest:IRequest<object>
    {
        public AttendanceFilterDto Filters {  get; set; }
    }
}
