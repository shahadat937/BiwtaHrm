using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features;
using Hrm.Application.DTOs.RoleDashboard;
using Hrm.Application.DTOs.RoleFeatures;
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
    public class GetRoleDashboardListRequestHandler : IRequestHandler<GetRoleDashboardListRequest, List<RoleDashboardDto>>
    {
        private readonly IHrmRepository<RoleDashboard> _RoleDashboardRepository;
        private readonly IHrmRepository<AspNetRoles> _AspNetRolesRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRoleDashboardListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleDashboard> RoleDashboardRepository, IHrmRepository<AspNetRoles> aspNetRolesRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleDashboardRepository = RoleDashboardRepository;
            _AspNetRolesRepository = aspNetRolesRepository;
        }

        public async Task<List<RoleDashboardDto>> Handle(GetRoleDashboardListRequest request, CancellationToken cancellationToken)
        {

            var allRoles = await _AspNetRolesRepository.GetAll();

            var roleDashboards = await _RoleDashboardRepository.GetAll();

            var result = allRoles.Select(role =>
            {
                var roleDashboard = roleDashboards.FirstOrDefault(rd => rd.RoleId == role.Id);

                return new RoleDashboardDto
                {
                    Id = roleDashboard?.Id ?? 0,
                    RoleId = role.Id,
                    RoleName = role.Name,
                    DashboardPermission = roleDashboard?.DashboardPermission ?? false,
                    EmpDashboardPermission = roleDashboard?.EmpDashboardPermission ?? false,
                    IsActive = roleDashboard?.IsActive ?? true
                };
            }).ToList();

            return result;
        }
    }

}
