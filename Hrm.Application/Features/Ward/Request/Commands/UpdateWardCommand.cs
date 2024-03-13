using Hrm.Application.DTOs.Ward;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Request.Commands
{
    public class UpdateWardCommand : IRequest<BaseCommandResponse>
    {
        public required WardDto WardDto { get; set; }
    }
}