using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.DTOs.ChildStatus;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Requests.Commands
{
    public class CreateChildStatusCommand : IRequest<BaseCommandResponse>
    {
        public CreateChildStatusDto ChildStatusDto { get; set; }
    }
}
