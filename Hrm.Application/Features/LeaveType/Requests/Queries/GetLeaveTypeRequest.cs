﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.LeaveType.Requests.Queries
{
    public class GetLeaveTypeRequest: IRequest<object>
    {
        public bool? ShowReport { get; set; }
    }
}
