using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Scales.Requests.Queries;
using Hrm.Application.DTOs.Scale;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Scales.Handlers.Queries
{
    public class GetScaleDetailRequestHandler : IRequestHandler<GetScaleDetailRequest, ScaleDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Scale> _ScaleRepository;
        public GetScaleDetailRequestHandler(IHrmRepository<Hrm.Domain.Scale> ScaleRepository, IMapper mapper)
        {
            _ScaleRepository = ScaleRepository;
            _mapper = mapper;
        }
        public async Task<ScaleDto> Handle(GetScaleDetailRequest request, CancellationToken cancellationToken)
        {
            var Scale = await _ScaleRepository.Get(request.ScaleId);
            return _mapper.Map<ScaleDto>(Scale);
        }
    }
}
