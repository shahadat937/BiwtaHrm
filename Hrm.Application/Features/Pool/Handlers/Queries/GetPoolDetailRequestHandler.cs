using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Pools.Requests.Queries;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Pools.Handlers.Queries
{
    public class GetPoolDetailRequestHandler : IRequestHandler<GetPoolDetailRequest, PoolDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Pool> _PoolRepository;
        public GetPoolDetailRequestHandler(IHrmRepository<Hrm.Domain.Pool> PoolRepository, IMapper mapper)
        {
            _PoolRepository = PoolRepository;
            _mapper = mapper;
        }
        public async Task<PoolDto> Handle(GetPoolDetailRequest request, CancellationToken cancellationToken)
        {
            var Pool = await _PoolRepository.Get(request.PoolId);
            return _mapper.Map<PoolDto>(Pool);
        }
    }
}
