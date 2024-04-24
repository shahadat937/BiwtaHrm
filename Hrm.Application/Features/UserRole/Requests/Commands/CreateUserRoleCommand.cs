using Hrm.Application.DTOs.UserRole;
using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRole.Requests.Commands
{
    public class CreateBloodCommand : IRequest<BaseCommandResponse>
    {
        public CreateUserRoleDto UserRoleDto { get; set; }
    }
}
