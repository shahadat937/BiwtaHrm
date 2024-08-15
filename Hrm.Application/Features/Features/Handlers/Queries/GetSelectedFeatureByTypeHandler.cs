using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Features.Requests.Queries;
using Hrm.Domain;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Features.Handlers.Queries
{
    public class GetSelectedFeatureByTypeHandler : IRequestHandler<GetSelectedFeatureRequests, List<SelectedModel>>
    {
        private readonly IHrmRepository<Feature> _FeatureRepository;


        public GetSelectedFeatureByTypeHandler(IHrmRepository<Feature> FeatureRepository)
        {
            _FeatureRepository = FeatureRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedFeatureRequests request, CancellationToken cancellationToken)
        {
            ICollection<Feature> Features = await _FeatureRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Features.Select(x => new SelectedModel
            {
                Name = x.FeatureName,
                Id = x.FeatureId
            }).ToList();
            return selectModels;
        }
    }
}
