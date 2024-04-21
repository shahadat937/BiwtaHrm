using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.DTOs.Pool;
using Hrm.Application.Exceptions;
using Hrm.Application.Features.Pool.Requests.Queries;
using Hrm.Application.Models;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Pool.Handlers.Queries
{
    public class GetPoolRequestHandler : IRequestHandler<GetPoolRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Pool> _PoolRepository;
        private readonly IMapper _mapper;
        public GetPoolRequestHandler(IHrmRepository<Hrm.Domain.Pool> PoolRepository, IMapper mapper)
        {
            _PoolRepository = PoolRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetPoolRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Pool> Pool = _PoolRepository.Where(x => true);
            Pool = Pool.OrderByDescending(x => x.PoolId);

            var PoolDtos = _mapper.Map<List<PoolDto>>(Pool);

            return PoolDtos;
        }
    }
}