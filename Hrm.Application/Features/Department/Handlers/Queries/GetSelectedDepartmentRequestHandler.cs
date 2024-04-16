using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Department.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Department.Handlers.Queries
{ 
    public class GetSelectedDepartmentRequestHandler : IRequestHandler<GetSelectedDepartmentRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Department> _DepartmentRepository;


        public GetSelectedDepartmentRequestHandler(IHrmRepository<Hrm.Domain.Department> DepartmentRepository)
        {
            _DepartmentRepository = DepartmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepartmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Department> Departments = await _DepartmentRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Departments.Select(x => new SelectedModel 
            {
                Name = x.DepartmentName,
                Id = x.DepartmentId
            }).ToList();
            return selectModels;
        }
    }
}
 