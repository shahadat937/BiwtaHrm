using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Country;
using Hrm.Application.DTOs.TrainingType;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.TrainingType.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Handlers.Queries
{
    public class GetCountryRequestHandler : IRequestHandler<GetCountryRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryRequestHandler(IHrmRepository<Hrm.Domain.Country> countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }


        public async Task<object> Handle(GetCountryRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Country> countries = _countryRepository.Where(x => true);
            var CountryDtos = await Task.Run(() => _mapper.Map<List<CountryDto>>(countries));

            return CountryDtos;
        }
    }
}