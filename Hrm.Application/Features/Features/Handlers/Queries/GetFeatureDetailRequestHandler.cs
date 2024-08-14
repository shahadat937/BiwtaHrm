using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Features;
using Hrm.Application.Features.Features.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.Features.Handlers.Queries
{
    public class GetFeaturesDetailRequestHandler : IRequestHandler<GetFeatureDetailRequest, FeatureDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Feature> _FeatureRepository;
        public GetFeaturesDetailRequestHandler(IHrmRepository<Feature> FeatureRepository, IMapper mapper)
        {
            _FeatureRepository = FeatureRepository;
            _mapper = mapper;
        }
        public async Task<FeatureDto> Handle(GetFeatureDetailRequest request, CancellationToken cancellationToken)
        {
            var Feature = await _FeatureRepository.Get(request.Id);
            return _mapper.Map<FeatureDto>(Feature);
        }
    }
}
