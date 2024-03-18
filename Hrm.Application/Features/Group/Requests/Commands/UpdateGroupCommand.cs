using Hrm.Application.DTOs.Group;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Requests.Commands
{
    public class UpdateGroupCommand : IRequest<Unit>
    {
        public GroupDto GroupDto { get; set; }
    }
}
