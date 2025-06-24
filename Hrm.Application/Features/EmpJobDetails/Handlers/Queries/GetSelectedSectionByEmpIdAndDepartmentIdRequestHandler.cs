using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.EmpJobDetails.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpJobDetails.Handlers.Queries
{
    public class GetSelectedSectionByEmpIdAndDepartmentIdRequestHandler : IRequestHandler<GetSelectedSectionByEmpIdAndDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _JobDetailsRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpOtherResponsibility> _OtherResponsibility;
        public GetSelectedSectionByEmpIdAndDepartmentIdRequestHandler(IHrmRepository<Hrm.Domain.EmpJobDetail> JobDetailsRepository, IHrmRepository<Hrm.Domain.EmpOtherResponsibility> OtherResponsibility)
        {
            _JobDetailsRepository = JobDetailsRepository;
            _OtherResponsibility = OtherResponsibility;
            
        }
        public async Task<List<SelectedModel>> Handle(GetSelectedSectionByEmpIdAndDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var selectDepartement = await _JobDetailsRepository
                .Where(x => x.EmpId == request.EmpId && x.DepartmentId == request.DepartmentId)
                .Select(x => new SelectedModel
                {
                    Id = x.SectionId,
                    Name = x.Section.SectionName
                })
                .ToListAsync(cancellationToken);

            var selectDepartementOther = await _OtherResponsibility
                .Where(x => x.EmpId == request.EmpId && x.DepartmentId == request.DepartmentId)
                .Select(x => new SelectedModel
                {
                    Id = x.SectionId,
                    Name = x.Section.SectionName
                })
                .ToListAsync(cancellationToken);

            var distinctDepartementOther = selectDepartementOther
                                          .GroupBy(x => x.Id)
                                          .Select(g => g.First())
                                          .ToList();

            var combined = selectDepartement
                .Concat(distinctDepartementOther
                    .Where(x => !selectDepartement.Any(d => d.Id == x.Id)))
                .ToList();

            return combined;
        }


    }
}
