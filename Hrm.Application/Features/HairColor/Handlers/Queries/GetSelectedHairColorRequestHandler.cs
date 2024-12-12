using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.HairColors.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.HairColors.Handlers.Queries
{ 
    public class GetSelectedHairColorRequestHandler : IRequestHandler<GetSelectedHairColorRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.HairColor> _HairColorRepository;


        public GetSelectedHairColorRequestHandler(IHrmRepository<Hrm.Domain.HairColor> HairColorRepository)
        {
            _HairColorRepository = HairColorRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedHairColorRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.HairColor> HairColors = await _HairColorRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = HairColors.Select(x => new SelectedModel 
            {
                Name = x.HairColorName,
                Id = x.HairColorId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 