using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Union;
using Hrm.Application.Features.Union.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Union.Handlers.Queries
{
    public class GetUnionRequestHandler : IRequestHandler<GetUnionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Union> _UnionRepository;
        private readonly IMapper _mapper;
        public GetUnionRequestHandler(IHrmRepository<Hrm.Domain.Union> UnionRepository, IMapper mapper)
        {
            _UnionRepository = UnionRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetUnionRequest request, CancellationToken cancellationToken)
        {
            //IQueryable<Hrm.Domain.Union> Union = _UnionRepository.Where(x => true);

            //var UnionDtos = _mapper.Map<List<UnionDto>>(Union);

            //return UnionDtos;



            IQueryable<Hrm.Domain.Union> Union = _UnionRepository.Where(x => true);

            // Use Task.Run to offload the synchronous operation to a background thread
            var UnionDtos = await Task.Run(() => _mapper.Map<List<UnionDto>>(Union));

            return UnionDtos;
        }
    }
}
