using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.PromotionTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PromotionTypes.Handlers.Queries
{ 
    public class GetSelectedPromotionTypeRequestHandler : IRequestHandler<GetSelectedPromotionTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.PromotionType> _PromotionTypeRepository;


        public GetSelectedPromotionTypeRequestHandler(IHrmRepository<Hrm.Domain.PromotionType> PromotionTypeRepository)
        {
            _PromotionTypeRepository = PromotionTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPromotionTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.PromotionType> PromotionTypes = await _PromotionTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = PromotionTypes.Select(x => new SelectedModel 
            {
                Name = x.PromotionTypeName,
                Id = x.PromotionTypeId
            }).ToList();
            return selectModels;
        }
    }
}
 