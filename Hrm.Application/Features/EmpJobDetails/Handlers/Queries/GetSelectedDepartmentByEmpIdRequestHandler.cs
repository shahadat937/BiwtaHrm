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
    public class GetSelectedDepartmentByEmpIdRequestHandler : IRequestHandler<GetSelectedDepartmentByEmpIdRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.EmpJobDetail> _JobDetailsRepository;
        private readonly IHrmRepository<Hrm.Domain.EmpOtherResponsibility> _OtherResponsibility;
        public GetSelectedDepartmentByEmpIdRequestHandler(IHrmRepository<Hrm.Domain.EmpJobDetail> JobDetailsRepository, IHrmRepository<Hrm.Domain.EmpOtherResponsibility> OtherResponsibility)
        {
            _JobDetailsRepository = JobDetailsRepository;
            _OtherResponsibility = OtherResponsibility;
            
        }
        public async Task<List<SelectedModel>> Handle(GetSelectedDepartmentByEmpIdRequest request, CancellationToken cancellationToken)
        {
            var selectDepartment = await _JobDetailsRepository
                .Where(x => x.EmpId == request.EmpId)
                .Select(x => new SelectedModel
                {
                    Id = x.DepartmentId,
                    Name = x.Department.DepartmentName
                })
                .ToListAsync(cancellationToken);

            var selectDepartmentOther = await _OtherResponsibility
                .Where(x => x.EmpId == request.EmpId)
                .Select(x => new SelectedModel
                {
                    Id = x.DepartmentId,
                    Name = x.Department.DepartmentName
                })
                .ToListAsync(cancellationToken);

            // Remove duplicates inside selectDepartmentOther
            var distinctDepartmentOther = selectDepartmentOther
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            var combined = selectDepartment
                .Concat(distinctDepartmentOther
                    .Where(x => !selectDepartment.Any(d => d.Id == x.Id)))
                .ToList();

            return combined;
        }


    }
}
