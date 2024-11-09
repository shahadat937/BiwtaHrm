using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpSpouseInfos.Requests.Commands;
using Hrm.Application.Features.RoleDashboards.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.RoleDashboards.Handlers.Commands
{
    public class CreateRoleDashboardCommandHandler : IRequestHandler<CreateRoleDashboardCommand, BaseCommandResponse>
    {

        private readonly IHrmRepository<RoleDashboard> _RoleDashboardRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleDashboardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IHrmRepository<RoleDashboard> RoleDashboardRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _RoleDashboardRepository = RoleDashboardRepository;
        }
        public async Task<BaseCommandResponse> Handle(CreateRoleDashboardCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();

            foreach (var item in request.RoleDashboardDtos)
            {
                if (item.Id == 0)
                {
                    RoleDashboard RoleDashboardInfo = new RoleDashboard
                    {
                        Id = item.Id,
                        RoleId = item.RoleId,
                        DashboardPermission = item.DashboardPermission,
                        EmpDashboardPermission = item.EmpDashboardPermission,
                        IsActive = item.IsActive,
                    };

                    await _unitOfWork.Repository<RoleDashboard>().Add(RoleDashboardInfo);
                }
                else
                {
                    var RoleDashboardInfo = await _unitOfWork.Repository<RoleDashboard>().Get(item.Id);

                    RoleDashboardInfo.Id = item.Id;
                    RoleDashboardInfo.RoleId = item.RoleId;
                    RoleDashboardInfo.DashboardPermission = item.DashboardPermission;
                    RoleDashboardInfo.EmpDashboardPermission = item.EmpDashboardPermission;
                    RoleDashboardInfo.IsActive = item.IsActive;

                    await _unitOfWork.Repository<RoleDashboard>().Update(RoleDashboardInfo);
                }
            }

            response.Success = true;
            response.Message = "Update Successful";

            await _unitOfWork.Save();


            return response;
        }
    }
}