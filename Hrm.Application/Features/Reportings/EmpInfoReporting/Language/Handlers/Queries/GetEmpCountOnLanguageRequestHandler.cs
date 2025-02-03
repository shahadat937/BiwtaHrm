using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Language.Handlers.Queries
{
    public class GetEmpCountOnLanguageRequestHandler : IRequestHandler<GetEmpCountOnLanguageRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.Language> _LanguageRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnLanguageRequestHandler(
            IHrmRepository<Domain.Language> LanguageRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _LanguageRepository = LanguageRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnLanguageRequest request, CancellationToken cancellationToken)
        {
            var Languages = await _LanguageRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpLanguageInfo.FirstOrDefault().LanguageId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpLanguageInfo.FirstOrDefault().LanguageId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var LanguageInfoList = new List<CountReportingInfo>();

            foreach (var Language in Languages)
            {
                var LanguageInfo = new CountReportingInfo
                {
                    Id = Language.LanguageId,
                    Name = Language.LanguageName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpLanguageInfo.Any(x=> x.LanguageId == Language.LanguageId) &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                LanguageInfoList.Add(LanguageInfo);
            }

            result.CountReportingInfo = LanguageInfoList.OrderBy(x => x.Name).ToList();

            return result;
        }
    }
}