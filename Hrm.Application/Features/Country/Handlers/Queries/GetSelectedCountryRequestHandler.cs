using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Countrys.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Countrys.Handlers.Queries
{ 
    public class GetSelectedCountryRequestHandler : IRequestHandler<GetSelectedCountryRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Country> _CountryRepository;


        public GetSelectedCountryRequestHandler(IHrmRepository<Hrm.Domain.Country> CountryRepository)
        {
            _CountryRepository = CountryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCountryRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Country> Countrys = await _CountryRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Countrys.Select(x => new SelectedModel 
            {
                Name = x.CountryName,
                Id = x.CountryId
            }).ToList();
            return selectModels;
        }
    }
}
 