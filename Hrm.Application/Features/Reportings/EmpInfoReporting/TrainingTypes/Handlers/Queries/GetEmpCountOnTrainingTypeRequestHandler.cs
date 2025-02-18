using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Reporting;
using Hrm.Application.Features.Reportings.EmpInfoReporting.TrainingTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Reportings.EmpInfoReporting.TrainingType.Handlers.Queries
{
    public class GetEmpCountOnTrainingTypeRequestHandler : IRequestHandler<GetEmpCountOnTrainingTypeRequest, EmpCountOnReportingDto>
    {
        private readonly IHrmRepository<Domain.TrainingType> _TrainingTypeRepository;
        private readonly IHrmRepository<EmpBasicInfo> _EmpBasicInfoRepository;

        public GetEmpCountOnTrainingTypeRequestHandler(
            IHrmRepository<Domain.TrainingType> TrainingTypeRepository, IHrmRepository<EmpBasicInfo> EmpBasicInfoRepository)
        {
            _TrainingTypeRepository = TrainingTypeRepository;
            _EmpBasicInfoRepository = EmpBasicInfoRepository;
        }

        public async Task<EmpCountOnReportingDto> Handle(GetEmpCountOnTrainingTypeRequest request, CancellationToken cancellationToken)
        {
            var TrainingTypes = await _TrainingTypeRepository.GetAll();
            var result = new EmpCountOnReportingDto();

            result.TotalAssigned = await _EmpBasicInfoRepository.CountAsync(x => x.EmpTrainingInfo.FirstOrDefault().TrainingTypeId != null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            result.TotalNull = await _EmpBasicInfoRepository.CountAsync(x => x.EmpTrainingInfo.FirstOrDefault().TrainingTypeId == null &&
                (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId));

            var TrainingTypeInfoList = new List<CountReportingInfo>();

            foreach (var TrainingType in TrainingTypes)
            {
                var TrainingTypeInfo = new CountReportingInfo
                {
                    Id = TrainingType.TrainingTypeId,
                    Name = TrainingType.TrainingTypeName ?? "",
                    Count = await _EmpBasicInfoRepository.CountAsync(x => x.EmpTrainingInfo.Any(x=> x.TrainingTypeId == TrainingType.TrainingTypeId) &&
                        (request.DepartmentId == 0 || x.EmpJobDetail.FirstOrDefault().DepartmentId == request.DepartmentId) &&
                        (request.SectionId == 0 || x.EmpJobDetail.FirstOrDefault().SectionId == request.SectionId))
                };
                TrainingTypeInfoList.Add(TrainingTypeInfo);
            }

            result.CountReportingInfo = TrainingTypeInfoList.OrderBy(x => x.Name).ToList();

            return result;
        }
    }
}