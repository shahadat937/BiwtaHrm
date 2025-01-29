using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.BloodGroups.Handlers.Queries
{
    public class GetEmpCountOnBloodGroupRequestHandler : IRequestHandler<GetEmpCountOnBloodGroupRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.BloodGroup> _BloodGroupRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnBloodGroupRequestHandler(
            IHrmRepository<Domain.BloodGroup> BloodGroupRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _BloodGroupRepository = BloodGroupRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnBloodGroupRequest request, CancellationToken cancellationToken)
        {
            var BloodGroups = await _BloodGroupRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroupId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));
            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroupId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var BloodGroupInfoList = new List<CountReportingInfo>();

            foreach (var BloodGroup in BloodGroups)
            {
                var BloodGroupInfo = new CountReportingInfo
                {
                    Id = BloodGroup.BloodGroupId,
                    Name = BloodGroup.BloodGroupName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpPersonalInfo.FirstOrDefault().BloodGroupId == BloodGroup.BloodGroupId &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                BloodGroupInfoList.Add(BloodGroupInfo);
            }

            result.CountReportingInfo = BloodGroupInfoList;

            return result;
        }
    }
}