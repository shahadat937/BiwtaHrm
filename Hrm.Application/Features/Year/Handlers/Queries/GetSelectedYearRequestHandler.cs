using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Year.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Year.Handlers.Queries
{ 
    public class GetSelectedYearRequestHandler : IRequestHandler<GetSelectedYearRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Year> _YearRepository;


        public GetSelectedYearRequestHandler(IHrmRepository<Hrm.Domain.Year> YearRepository)
        {
            _YearRepository = YearRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedYearRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Year> Years = await _YearRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Years.Select(x => new SelectedModel 
            {
                Name = x.YearName.ToString(),
                Id = x.YearId
            }).ToList();
            return selectModels;
        }
    }
}
 