using Hrm.Application.DTOs.RoleDashboard;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleDashboards.Requests.Queries
{
    public class GetRoleDashboardPermissionByRoleRequest : IRequest<RoleDashboardDto>
    {
        public string RoleName { get; set; }
    }
}
