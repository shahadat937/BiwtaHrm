using Hrm.Application.DTOs.AspNetUserRoles;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.DTOs.UserRole;
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
    public class UpdateUserRoleCommand : IRequest<BaseCommandResponse>
    {
        public AspNetUserRolesDto UserRoleDto { get; set; }
    }
}
