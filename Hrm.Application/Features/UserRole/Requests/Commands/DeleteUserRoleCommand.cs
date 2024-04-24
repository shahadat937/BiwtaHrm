using Hrm.Application.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.UserRole.Requests.Commands
{
    public class DeleteUserRoleCommand : IRequest<BaseCommandResponse>
    {
        public int UserRoleId { get; set; }
    }
}
