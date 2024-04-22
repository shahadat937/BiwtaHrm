using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Pools.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pools.Handlers.Queries
{ 
    public class GetSelectedPoolRequestHandler : IRequestHandler<GetSelectedPoolRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Pool> _PoolRepository;


        public GetSelectedPoolRequestHandler(IHrmRepository<Hrm.Domain.Pool> PoolRepository)
        {
            _PoolRepository = PoolRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPoolRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Pool> Pools = await _PoolRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Pools.Select(x => new SelectedModel 
            {
                Name = x.PoolName,
                Id = x.PoolId
            }).ToList();
            return selectModels;
        }
    }
}
 