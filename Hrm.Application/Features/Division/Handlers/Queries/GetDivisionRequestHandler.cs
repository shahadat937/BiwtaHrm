using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.Division;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Division.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Division.Handlers.Queries
{
    public class GetDivisionRequestHandler : IRequestHandler<GetDivisionRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.Division> _DivisionRepository;
        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IMapper _mapper;
        public GetDivisionRequestHandler(IHrmRepository<Hrm.Domain.Division> DivisionRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository)
        {
            _DivisionRepository = DivisionRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<object> Handle(GetDivisionRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.Division> divisions = _DivisionRepository.FilterWithInclude(x => true);
            divisions = divisions.OrderByDescending(x => x.DivisionId);

            var divisionDtos = new List<DivisionDto>();

            foreach (var division in divisions)
            {
                var divisionDto = _mapper.Map<DivisionDto>(division);
                var countryName = await GetCountryName(division.CountryId);
                divisionDto.CountryName = countryName;
                divisionDtos.Add(divisionDto);
            }

            return divisionDtos;
        }

        private async Task<string?> GetCountryName(int? countryId)
        {
            if (countryId.HasValue)
            {
                var country = await _countryRepository.Get(countryId.Value);
                return country?.CountryName;
            }
            return null;
        }
    }
}
