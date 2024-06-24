using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Relation;
using Hrm.Application.Features.Relation.Requests.Queries;
using Hrm.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Relation.Handlers.Queries
{
    public class GetRelationRequestHandler : IRequestHandler<GetRelationRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Relation> _RelationRepository;
        private readonly IMapper _mapper;
        public GetRelationRequestHandler(IHrmRepository<Hrm.Domain.Relation> RelationRepository, IMapper mapper)
        {
            _RelationRepository = RelationRepository;
            _mapper = mapper;
        }

        public async Task<object> Handle(GetRelationRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Relation> Relation = _RelationRepository.Where(x => true);
            Relation = Relation.OrderByDescending(x => x.RelationId);

            var RelationDtos = _mapper.Map<List<RelationDto>>(Relation);

            return RelationDtos;
        }
    }
}
