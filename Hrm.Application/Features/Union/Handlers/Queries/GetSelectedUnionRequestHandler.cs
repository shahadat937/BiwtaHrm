using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Queries
{ 
    public class GetSelectedUnionRequestHandler : IRequestHandler<GetSelectedUnionRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository;


        public GetSelectedUnionRequestHandler(IHrmRepository<Hrm.Domain.Union> UnionRepository)
        {
            _UnionRepository = UnionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedUnionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Union> Unions = await _UnionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Unions.Select(x => new SelectedModel 
            {
                Name = x.UnionName,
                Id = x.UnionId
            }).ToList();
            return selectModels;
        }
    }
}
 