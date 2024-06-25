using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Overall_EV_Promotions.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Overall_EV_Promotions.Handlers.Queries
{ 
    public class GetSelectedOverall_EV_PromotionRequestHandler : IRequestHandler<GetSelectedOverall_EV_PromotionRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.OverallEVPromotion> _Overall_EV_PromotionRepository;


        public GetSelectedOverall_EV_PromotionRequestHandler(IHrmRepository<Hrm.Domain.OverallEVPromotion> Overall_EV_PromotionRepository)
        {
            _Overall_EV_PromotionRepository = Overall_EV_PromotionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOverall_EV_PromotionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.OverallEVPromotion> Overall_EV_Promotions = await _Overall_EV_PromotionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Overall_EV_Promotions.Select(x => new SelectedModel 
            {
                Name = x.OverallEVPromotionName,
                Id = x.OverallEVPromotionId
            }).ToList();
            return selectModels;
        }
    }
}
 