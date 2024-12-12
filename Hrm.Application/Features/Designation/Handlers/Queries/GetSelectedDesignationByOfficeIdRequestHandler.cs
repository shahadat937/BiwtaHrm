using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
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


            IQueryable<Hrm.Domain.Designation> designationQuery = _DesignationRepository.Where(x => x.OfficeId == request.OfficeId && x.DepartmentId == null && !empJobDetailDesignationIds.Contains(x.DesignationId))
                    .Include(x => x.DesignationSetup);

            List<Hrm.Domain.Designation> designations = await designationQuery.ToListAsync(cancellationToken);

            List<SelectedModel> selectModels = designations
                .GroupBy(x => x.DesignationSetupId)
                .Select(x => x.FirstOrDefault())
                .Select(x => new SelectedModel
                {
                    Name = x.DesignationSetup.Name,
                    Id = x.DesignationId
                }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}