using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Queries
{ 
    public class GetSelectedDivisionRequestHandler : IRequestHandler<GetSelectedDivisionRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;


        public GetSelectedDivisionRequestHandler(IHrmRepository<Hrm.Domain.Division> DivisionRepository)
        {
            _DivisionRepository = DivisionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDivisionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Division> Divisions = await _DivisionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Divisions.Select(x => new SelectedModel 
            {
                Name = x.DivisionName,
                Id = x.DivisionId
            }).OrderBy(x => x.Name).ToList();
            return selectModels;
        }
    }
}
 