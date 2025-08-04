using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Country;
using Hrm.Application.Features.Country.Requests.Queries;
using Hrm.Application.Features.Countrys.Requests.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Country.Handlers.Queries
{
    public class GetDefaultCountryIdRequestHandler : IRequestHandler<GetDefaultCountryIdRequest, int>
    {
        // private readonly IUTOfficerTypeRepository _UTOfficerTypeRepository;
        private readonly IMapper _mapper;
        private readonly IHrmRepository<Hrm.Domain.Country> _CountryRepository;
        public GetDefaultCountryIdRequestHandler(IHrmRepository<Hrm.Domain.Country> CountryRepository, IMapper mapper)
        {
            _CountryRepository = CountryRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(GetDefaultCountryIdRequest request, CancellationToken cancellationToken)
        {
            var defaultCountry = await _CountryRepository.Where(x => x.IsDefault == true).FirstOrDefaultAsync();
            return defaultCountry.CountryId;
        }
    }
}
