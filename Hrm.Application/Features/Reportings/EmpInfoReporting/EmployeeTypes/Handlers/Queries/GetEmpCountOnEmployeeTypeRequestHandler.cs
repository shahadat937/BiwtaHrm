using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Dashboard.Chart;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Dashboards.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.EmployeeTypes.Handlers.Queries
{
    public class GetEmpCountOnEmployeeTypeRequestHandler : IRequestHandler<GetEmpCountOnEmployeeTypeRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.EmployeeType> _EmployeeTypeRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnEmployeeTypeRequestHandler(
            IHrmRepository<Domain.EmployeeType> EmployeeTypeRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _EmployeeTypeRepository = EmployeeTypeRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnEmployeeTypeRequest request, CancellationToken cancellationToken)
        {
            var employeeTypes = await _EmployeeTypeRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmployeeTypeId != null && 
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmployeeTypeId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var employeeTypeInfoList = new List<CountReportingInfo>();

            foreach (var employeeType in employeeTypes)
            {
                var employeeTypeInfo = new CountReportingInfo
                {
                    Id = employeeType.EmployeeTypeId,
                    Name = employeeType.EmployeeTypeName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmployeeTypeId == employeeType.EmployeeTypeId &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                employeeTypeInfoList.Add(employeeTypeInfo);
            }
            
            result.CountReportingInfo = employeeTypeInfoList.OrderBy(x => x.Name).ToList();

            return result;
        }
    }
}