using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Overall_EV_Promotions.Requests.Queries;
using Hrm.Application.DTOs.Overall_EV_Promotion;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Overall_EV_Promotions.Handlers.Queries
{
    public class GetOverall_EV_PromotionDetailRequestHandler : IRequestHandler<GetOverall_EV_PromotionDetailRequest, Overall_EV_PromotionDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.OverallEVPromotion> _Overall_EV_PromotionRepository;
        public GetOverall_EV_PromotionDetailRequestHandler(IHrmRepository<Hrm.Domain.OverallEVPromotion> Overall_EV_PromotionRepository, IMapper mapper)
        {
            _Overall_EV_PromotionRepository = Overall_EV_PromotionRepository;
            _mapper = mapper;
        }
        public async Task<Overall_EV_PromotionDto> Handle(GetOverall_EV_PromotionDetailRequest request, CancellationToken cancellationToken)
        {
            var Overall_EV_Promotion = await _Overall_EV_PromotionRepository.Get(request.Overall_EV_PromotionId);
            return _mapper.Map<Overall_EV_PromotionDto>(Overall_EV_Promotion);
        }
    }
}
