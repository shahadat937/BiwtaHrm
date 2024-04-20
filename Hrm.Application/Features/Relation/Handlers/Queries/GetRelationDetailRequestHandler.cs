using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Relations.Requests.Queries;
using Hrm.Application.DTOs.Relation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Relations.Handlers.Queries
{
    public class GetRelationDetailRequestHandler : IRequestHandler<GetRelationDetailRequest, RelationDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Relation> _RelationRepository;
        public GetRelationDetailRequestHandler(IHrmRepository<Hrm.Domain.Relation> RelationRepository, IMapper mapper)
        {
            _RelationRepository = RelationRepository;
            _mapper = mapper;
        }
        public async Task<RelationDto> Handle(GetRelationDetailRequest request, CancellationToken cancellationToken)
        {
            var Relation = await _RelationRepository.Get(request.RelationId);
            return _mapper.Map<RelationDto>(Relation);
        }
    }
}
