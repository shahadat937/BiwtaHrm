using Hrm.Application.DTOs.ChildStatus;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ChildStatus.Requests.Commands
{
    public class UpdateChildStatusCommand : IRequest<Unit>
    {
        public ChildStatusDto ChildStatusDto { get; set; }
    }
}
