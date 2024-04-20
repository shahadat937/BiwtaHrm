using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Occupations.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Occupations.Handlers.Queries
{ 
    public class GetSelectedOccupationRequestHandler : IRequestHandler<GetSelectedOccupationRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Occupation> _OccupationRepository;


        public GetSelectedOccupationRequestHandler(IHrmRepository<Hrm.Domain.Occupation> OccupationRepository)
        {
            _OccupationRepository = OccupationRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOccupationRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Occupation> Occupations = await _OccupationRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Occupations.Select(x => new SelectedModel 
            {
                Name = x.OccupationName,
                Id = x.OccupationId
            }).ToList();
            return selectModels;
        }
    }
}
 