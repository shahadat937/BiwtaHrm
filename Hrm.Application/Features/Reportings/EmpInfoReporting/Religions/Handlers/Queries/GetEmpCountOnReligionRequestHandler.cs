using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Requests.Queries;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Religions.Handlers.Queries
{
    public class GetEmpCountOnReligionRequestHandler : IRequestHandler<GetEmpCountOnReligionRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.Religion> _ReligionRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnReligionRequestHandler(
            IHrmRepository<Domain.Religion> ReligionRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _ReligionRepository = ReligionRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnReligionRequest request, CancellationToken cancellationToken)
        {
            var Religions = await _ReligionRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().ReligionId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().ReligionId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var ReligionInfoList = new List<CountReportingInfo>();

            foreach (var Religion in Religions)
            {
                var ReligionInfo = new CountReportingInfo
                {
                    Id = Religion.ReligionId,
                    Name = Religion.ReligionName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().ReligionId == Religion.ReligionId &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                ReligionInfoList.Add(ReligionInfo);
            }
            result.CountReportingInfo = ReligionInfoList.OrderBy(x => x.Name).ToList();

            return result;
        }
    }
}