using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.MaritalStatus.Handlers.Queries
{
    public class GetEmpCountOnMaritalStatusRequestHandler : IRequestHandler<GetEmpCountOnMaritalStatusRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.MaritalStatus> _MaritalStatusRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnMaritalStatusRequestHandler(
            IHrmRepository<Domain.MaritalStatus> MaritalStatusRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _MaritalStatusRepository = MaritalStatusRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnMaritalStatusRequest request, CancellationToken cancellationToken)
        {
            var MaritalStatuss = await _MaritalStatusRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().MaritalStatusId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().MaritalStatusId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var MaritalStatusInfoList = new List<CountReportingInfo>();

            foreach (var MaritalStatus in MaritalStatuss)
            {
                var MaritalStatusInfo = new CountReportingInfo
                {
                    Id = MaritalStatus.MaritalStatusId,
                    Name = MaritalStatus.MaritalStatusName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().MaritalStatusId == MaritalStatus.MaritalStatusId &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                MaritalStatusInfoList.Add(MaritalStatusInfo);
            }

            result.CountReportingInfo = MaritalStatusInfoList.OrderBy(x => x.Name).ToList();

            return result;
        }
    }
}