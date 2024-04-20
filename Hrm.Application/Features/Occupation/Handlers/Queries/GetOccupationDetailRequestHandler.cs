using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Occupations.Requests.Queries;
using Hrm.Application.DTOs.Occupation;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Occupations.Handlers.Queries
{
    public class GetOccupationDetailRequestHandler : IRequestHandler<GetOccupationDetailRequest, OccupationDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Occupation> _OccupationRepository;
        public GetOccupationDetailRequestHandler(IHrmRepository<Hrm.Domain.Occupation> OccupationRepository, IMapper mapper)
        {
            _OccupationRepository = OccupationRepository;
            _mapper = mapper;
        }
        public async Task<OccupationDto> Handle(GetOccupationDetailRequest request, CancellationToken cancellationToken)
        {
            var Occupation = await _OccupationRepository.Get(request.OccupationId);
            return _mapper.Map<OccupationDto>(Occupation);
        }
    }
}
