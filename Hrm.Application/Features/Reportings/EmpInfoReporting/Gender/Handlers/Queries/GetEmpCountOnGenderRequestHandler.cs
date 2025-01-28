using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.Gender.Handlers.Queries
{
    public class GetEmpCountOnGenderRequestHandler : IRequestHandler<GetEmpCountOnGenderRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.Gender> _GenderRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnGenderRequestHandler(
            IHrmRepository<Domain.Gender> GenderRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _GenderRepository = GenderRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnGenderRequest request, CancellationToken cancellationToken)
        {
            var Genders = await _GenderRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().GenderId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().GenderId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var GenderInfoList = new List<CountReportingInfo>();

            foreach (var Gender in Genders)
            {
                var GenderInfo = new CountReportingInfo
                {
                    Id = Gender.GenderId,
                    Name = Gender.GenderName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().GenderId == Gender.GenderId &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                GenderInfoList.Add(GenderInfo);
            }

            result.CountReportingInfo = GenderInfoList;

            return result;
        }
    }
}