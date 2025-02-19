﻿using Hrm.Application.DTOs.Office;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Office.Requests.Commands
{
    public class CreateOfficeCommand :IRequest<BaseCommandResponse>
    {
        public CreateOfficeDto OfficeDto { get; set; }
    }
}
