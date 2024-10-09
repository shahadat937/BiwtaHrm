using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Designation.Requests.Queries;
using Hrm.Application.Features.Designations.Requests.Queries;
using Hrm.Domain;
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
    public class GetSelectedDesignationByDepartmentIdRequestHandler : IRequestHandler<GetSelectedDesignationByDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Designation> _DesignationRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _EmpJobDetailRepository;
        private readonly IHrmRepository<EmpOtherResponsibility> _EmpOtherResponsibilityRepository;


        public GetSelectedDesignationByDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.Designation> DesignationRepository, IHrmRepository<Domain.EmpJobDetail> empJobDetailRepository, IHrmRepository<EmpOtherResponsibility> EmpOtherResponsibilityRepository)
        {
            _DesignationRepository = DesignationRepository;
            _EmpJobDetailRepository = empJobDetailRepository;
            _EmpOtherResponsibilityRepository = EmpOtherResponsibilityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDesignationByDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var empId = await _EmpJobDetailRepository.FindOneAsync(x => x.Id == request.EmpJobDetailId);

            ICollection<Hrm.Domain.EmpJobDetail> empJobDetails = await _EmpJobDetailRepository.FilterAsync(x => x.Id != request.EmpJobDetailId && x.ServiceStatus == true);

            var empJobDetailDesignationIds = empJobDetails.Select(e => e.DesignationId).ToHashSet();


            if (empId != null)
            {
                ICollection<EmpOtherResponsibility> otherResponsibilities = await _EmpOtherResponsibilityRepository.FilterAsync(x => x.EmpId != empId.EmpId && x.ServiceStatus == true);
                var empOtherResponsibilityDesignationIds = otherResponsibilities.Select(x => x.DesignationId).ToHashSet();

                IQueryable<Hrm.Domain.Designation> designations = _DesignationRepository.Where(x => x.DepartmentId == request.DepartmentId && x.SectionId == null && !empJobDetailDesignationIds.Contains(x.DesignationId) && !empOtherResponsibilityDesignationIds.Contains(x.DesignationId))
                    .Include(x => x.DesignationSetup);

                List<Hrm.Domain.Designation> designationList = await designations.ToListAsync(cancellationToken);

                List<SelectedModel> selectModels = designationList
                    .GroupBy(x => x.DesignationSetupId)
                    .Select(x => x.FirstOrDefault())
                    .Select(x => new SelectedModel
                    {
                        Name = x.DesignationSetup.Name,
                        Id = x.DesignationId
                    }).ToList();

                return selectModels;
            }

            else
            {
                IQueryable<Hrm.Domain.Designation> designationQuery = _DesignationRepository
                    .Where(x => x.DepartmentId == request.DepartmentId && x.SectionId == null && !empJobDetailDesignationIds.Contains(x.DesignationId))
                    .Include(x => x.DesignationSetup); 

                List<Hrm.Domain.Designation> designations = await designationQuery.ToListAsync(cancellationToken);

                List<SelectedModel> selectModels = designations
                    .GroupBy(x => x.DesignationSetupId)
                    .Select(x => x.FirstOrDefault())
                    .Select(x => new SelectedModel
                    {
                        Name = x.DesignationSetup.Name,
                        Id = x.DesignationId
                    }).ToList();

                return selectModels;
            }


            


        }
    }
}
