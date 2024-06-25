
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Hrm.Application.DTOs.UserRole;

namespace Hrm.Application.Features.UserRoles.Requests.Queries
{
    public class GetUserRoleDetailRequest : IRequest<UserRoleDto>
    {
        public int UserRoleId { get; set; }
    }
}
