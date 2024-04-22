using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SubDepartment.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubDepartment.Handlers.Queries
{ 
    public class GetSelectedSubDepartmentRequestHandler : IRequestHandler<GetSelectedSubDepartmentRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.SubDepartment> _SubDepartmentRepository;


        public GetSelectedSubDepartmentRequestHandler(IHrmRepository<Hrm.Domain.SubDepartment> SubDepartmentRepository)
        {
            _SubDepartmentRepository = SubDepartmentRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSubDepartmentRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.SubDepartment> SubDepartments = await _SubDepartmentRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = SubDepartments.Select(x => new SelectedModel 
            {
                Name = x.SubDepartmentName,
                Id = x.SubDepartmentId
            }).ToList();
            return selectModels;
        }
    }
}
 