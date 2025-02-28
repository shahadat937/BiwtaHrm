﻿using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.Leave;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Leave.Requests.Commands
{
    public class CreateLeaveCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveDto LeaveDto { get; set; }
    }
}
