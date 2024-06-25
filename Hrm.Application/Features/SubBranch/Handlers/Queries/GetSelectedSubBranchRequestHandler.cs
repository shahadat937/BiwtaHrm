using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.SubBranch.Handlers.Queries
{ 
    public class GetSelectedSubBranchRequestHandler : IRequestHandler<GetSelectedSubBranchRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.SubBranch> _SubBranchRepository;


        public GetSelectedSubBranchRequestHandler(IHrmRepository<Hrm.Domain.SubBranch> SubBranchRepository)
        {
            _SubBranchRepository = SubBranchRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSubBranchRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.SubBranch> SubBranchs = await _SubBranchRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = SubBranchs.Select(x => new SelectedModel 
            {
                Name = x.SubBranchName,
                Id = x.SubBranchId
            }).ToList();
            return selectModels;
        }
    }
}
 