using AutoMapper;

using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Hrm.Application.Features.Countrys.Requests.Queries;
using Hrm.Application.DTOs.Country;
using Hrm.Application.Contracts.Persistence;
using Hrm.Domain;

namespace Hrm.Application.Features.Countrys.Handlers.Queries
{
    public class GetCountryDetailRequestHandler : IRequestHandler<GetCountryDetailRequest, CountryDto>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Country> _CountryRepository;
        public GetCountryDetailRequestHandler(IHrmRepository<Hrm.Domain.Country> CountryRepository, IMapper mapper)
        {
            _CountryRepository = CountryRepository;
            _mapper = mapper;
        }
        public async Task<CountryDto> Handle(GetCountryDetailRequest request, CancellationToken cancellationToken)
        {
            var Country = await _CountryRepository.Get(request.CountryId);
            return _mapper.Map<CountryDto>(Country);
        }
    }
}
