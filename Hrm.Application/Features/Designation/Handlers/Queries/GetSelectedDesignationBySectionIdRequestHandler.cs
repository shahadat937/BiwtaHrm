using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetSelectedDesignationBySectionIdRequestHandler : IRequestHandler<GetSelectedDesignationBySectionIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;


        public GetSelectedDesignationBySectionIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IHrmRepository<Domain.EmpJobDetail> empJobDetailRepository, IHrmRepository<EmpOtherResponsibility> EmpOtherResponsibilityRepository)
        {
            _DesignationRepository = DesignationRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
            _EmpOtherResponsibilityRepository = EmpOtherResponsibilityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationBySectionIdRequest request, CancellationToken cancellationToken)
        {

            var empId = _EmpJobDetailRepository.FindOne(x => x.Id == request.EmpJobDetailId).EmpId;

            ICollection<Hrm.Domain.EmpJobDetail> empJobDetails = await _EmpJobDetailRepository.FilterAsync(x => x.Id != request.EmpJobDetailId && x.ServiceStatus == true);
            var empJobDetailDesignationIds = empJobDetails.Select(e => e.DesignationId).ToHashSet();

            ICollection<EmpOtherResponsibility> otherResponsibilities = await _EmpOtherResponsibilityRepository.FilterAsync(x => x.EmpId != empId && x.ServiceStatus == true);

            var empOtherResponsibilityDesignationIds = otherResponsibilities.Select(x => x.DesignationId).ToHashSet();

            ICollection<Hrm.Domain.Designation> designations = await _DesignationRepository.FilterAsync(x => x.SectionId == request.SectionId && !empJobDetailDesignationIds.Contains(x.DesignationId) && !empOtherResponsibilityDesignationIds.Contains(x.DesignationId));


            List<SelectedModel> selectModels = designations
                .GroupBy(x => x.DesignationName)
                .Select(x => x.FirstOrDefault())
                .Select(x => new SelectedModel
            {
                Name = x.DesignationName,
                Id = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}
