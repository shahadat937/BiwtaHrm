
using Hrm.Application.DTOs.AspNetUserRoles;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrm.Application.Features.UserRoles.Requests.Queries
{
    public class GetUserRoleDetailRequest : IRequest<AspNetUserRolesDto>
    {
        public string UserId { get; set; }
    }
}
