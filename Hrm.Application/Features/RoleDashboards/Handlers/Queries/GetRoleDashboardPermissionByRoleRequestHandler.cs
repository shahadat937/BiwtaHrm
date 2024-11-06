using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.RoleDashboard;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.RoleDashboards.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleDashboards.Handlers.Queries
{
    public class GetRoleDashboardPermissionByRoleRequestHandler : IRequestHandler<GetRoleDashboardPermissionByRoleRequest, object>
    {
        private readonly IHrmRepository<RoleDashboard> _RoleDashboardRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        public GetRoleDashboardPermissionByRoleRequestHandler(IHrmRepository<RoleDashboard> RoleDashboardRepository, IHrmRepository<AspNetRoles> AspNetRolesRepository)
        {
            _AspNetRolesRepository = AspNetRolesRepository;
            _RoleDashboardRepository = RoleDashboardRepository;
        }

        public async Task<object> Handle(GetRoleDashboardPermissionByRoleRequest request, CancellationToken cancellationToken)
        {
            var roleId = await _AspNetRolesRepository.FindOneAsync(x => x.Name == request.RoleName);

            var roleDashboard = await _RoleDashboardRepository.Where(x => x.RoleId == roleId.Id).FirstOrDefaultAsync();

            if(roleDashboard != null)
            {
                var result = new RoleDashboardDto
                {
                    DashboardPermission = roleDashboard.DashboardPermission,
                    EmpDashboardPermission = roleDashboard.EmpDashboardPermission,
                };
                return result;
            }

            return null;

        }
    }
}
