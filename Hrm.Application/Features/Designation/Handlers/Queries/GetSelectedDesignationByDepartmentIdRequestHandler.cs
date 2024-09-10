using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Designation.Handlers.Queries
{
    public class GetSelectedDesignationByDepartmentIdRequestHandler : IRequestHandler<GetSelectedDesignationByDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepository;


        public GetSelectedDesignationByDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IHrmRepository<Domain.EmpJobDetail> empJobDetailRepository)
        {
            _DesignationRepository = DesignationRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationByDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.EmpJobDetail> empJobDetails = await _EmpJobDetailRepository.FilterAsync(x => x.Id != request.EmpJobDetailId && x.ServiceStatus == true);
            var empJobDetailDesignationIds = empJobDetails.Select(e => e.DesignationId).ToHashSet();


            ICollection<Hrm.Domain.Designation> designations = await _DesignationRepository.FilterAsync(x => x.DepartmentId == request.DepartmentId && !empJobDetailDesignationIds.Contains(x.DesignationId));

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
