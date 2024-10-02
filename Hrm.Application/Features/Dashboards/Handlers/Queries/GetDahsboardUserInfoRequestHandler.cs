using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Widgets;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Dashboards.Handlers.Queries
{
    public class GetDahsboardUserInfoRequestHandler : IRequestHandler<GetDahsboardUserInfoRequest, object>
    {
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;
        private readonly IHrmRepository<Domain.EmployeeType> _EmployeeTypeRepository;
        private readonly IMapper _mapper;

        public GetDahsboardUserInfoRequestHandler(
            IMapper mapper,
            IHrmRepository<EmpBasicInfo> empBasicInfoRepository,
            IHrmRepository<Domain.EmployeeType> employeeTypeRepository)
        {
            _mapper = mapper;
            _EmpBasicInfoRepository = empBasicInfoRepository;
            _EmployeeTypeRepository = employeeTypeRepository;

        }

        public async Task<object> Handle(GetDahsboardUserInfoRequest request, CancellationToken cancellationToken)
        {
            var totalEmployees = await _EmpBasicInfoRepository.CountAsync(e => true);
            var employeeTypes = await _EmployeeTypeRepository.GetAll();

            var widgets = new List<WidgetsDto>
            {
                new WidgetsDto
                {
                    WidgetName = "Employees",
                    Label = "Total Employees",
                    Labels = new List<string>(),
                    Data = new List<int> { totalEmployees }
                }
            };


            foreach (var employeeType in employeeTypes)
            {
                // Count employees by EmployeeTypeId
                var employeeCountByType = await _EmpBasicInfoRepository.CountAsync(e => e.EmployeeTypeId == employeeType.EmployeeTypeId);

                widgets.Add(new WidgetsDto
                {
                    WidgetName = $"{employeeType.EmployeeTypeName} Employees",
                    Label = $"Total {employeeType.EmployeeTypeName} Employees",
                    Labels = new List<string>(),
                    Data = new List<int> { employeeCountByType }
                });
            }

            return widgets;
        }
    }
}
