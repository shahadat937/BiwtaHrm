using Hrm.Application.DTOs.Group;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Group.Requests.Commands
{
    public class UpdateGroupCommand : IRequest<BaseCommandResponse>
    {
        public GroupDto GroupDto { get; set; }
    }
}
