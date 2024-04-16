using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Branch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Branch.Handlers.Queries
{ 
    public class GetSelectedBranchRequestHandler : IRequestHandler<GetSelectedBranchRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Branch> _BranchRepository;


        public GetSelectedBranchRequestHandler(IHrmRepository<Hrm.Domain.Branch> BranchRepository)
        {
            _BranchRepository = BranchRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedBranchRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Branch> Branchs = await _BranchRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Branchs.Select(x => new SelectedModel 
            {
                Name = x.BranchName,
                Id = x.BranchId
            }).ToList();
            return selectModels;
        }
    }
}
 