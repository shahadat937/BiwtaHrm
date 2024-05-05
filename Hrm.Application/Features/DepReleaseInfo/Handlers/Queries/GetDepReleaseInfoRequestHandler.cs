using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.DepReleaseInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Queries
{
    public class GetDepReleaseInfoRequestHandler : IRequestHandler<GetDepReleaseInfoRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;
        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IMapper _mapper;
        public GetDepReleaseInfoRequestHandler(IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository)
        {
            _DepReleaseInfoRepository = DepReleaseInfoRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<object> Handle(GetDepReleaseInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = _DepReleaseInfoRepository.FilterWithInclude(x => true);
            DepReleaseInfos = DepReleaseInfos.OrderByDescending(x => x.DepReleaseInfoId);

            var DepReleaseInfoDtos = new List<DepReleaseInfoDto>();

            foreach (var DepReleaseInfo in DepReleaseInfos)
            {
                var DepReleaseInfoDto = _mapper.Map<DepReleaseInfoDto>(DepReleaseInfo);
               // var countryName = await GetCountryName(DepReleaseInfo.CountryId);
               // DepReleaseInfoDto.CountryName = countryName;
                DepReleaseInfoDtos.Add(DepReleaseInfoDto);
            }

            return DepReleaseInfoDtos;
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
