using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetSelectedDesignationByOfficeIdRequestHandler : IRequestHandler<GetSelectedDesignationByOfficeIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepository;


        public GetSelectedDesignationByOfficeIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IHrmRepository<Domain.EmpJobDetail> empJobDetailRepository)
        {
            _DesignationRepository = DesignationRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationByOfficeIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.EmpJobDetail> empJobDetails = await _EmpJobDetailRepository.FilterAsync(x => x.Id != request.EmpJobDetailId && x.ServiceStatus == true);

            var empJobDetailDesignationIds = empJobDetails.Select(e => e.DesignationId).ToHashSet();


            ICollection<Hrm.Domain.Designation> designations = await _DesignationRepository.FilterAsync(x => x.OfficeId == request.OfficeId && x.DepartmentId == null && !empJobDetailDesignationIds.Contains(x.DesignationId));

            List<SelectedModel> selectModels = designations.Select(x => new SelectedModel
            {
                Name = x.DesignationName,
                Id = x.DesignationId
            }).ToList();
            return selectModels;
        }
    }
}