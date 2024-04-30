using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.EmpTnsferPostingJoin;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.EmpTnsferPostingJoin.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.EmpTnsferPostingJoin.Handlers.Queries
{
    public class GetEmpTnsferPostingJoinRequestHandler : IRequestHandler<GetEmpTnsferPostingJoinRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> _EmpTnsferPostingJoinRepository;
        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IMapper _mapper;
        public GetEmpTnsferPostingJoinRequestHandler(IHrmRepository<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoinRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository)
        {
            _EmpTnsferPostingJoinRepository = EmpTnsferPostingJoinRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<object> Handle(GetEmpTnsferPostingJoinRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.EmpTnsferPostingJoin> EmpTnsferPostingJoins = _EmpTnsferPostingJoinRepository.FilterWithInclude(x => true);
            EmpTnsferPostingJoins = EmpTnsferPostingJoins.OrderByDescending(x => x.EmpTnsferPostingJoinId);

            var EmpTnsferPostingJoinDtos = new List<EmpTnsferPostingJoinDto>();

            //foreach (var EmpTnsferPostingJoin in EmpTnsferPostingJoins)
            //{
            //    var EmpTnsferPostingJoinDto = _mapper.Map<EmpTnsferPostingJoinDto>(EmpTnsferPostingJoin);
            //    var countryName = await GetCountryName(EmpTnsferPostingJoin.CountryId);
            //    EmpTnsferPostingJoinDto.CountryName = countryName;
            //    EmpTnsferPostingJoinDtos.Add(EmpTnsferPostingJoinDto);
            //}

            return EmpTnsferPostingJoinDtos;
        }

        //private async Task<string?> GetCountryName(int? countryId)
        //{
        //    if (countryId.HasValue)
        //    {
        //        var country = await _countryRepository.Get(countryId.Value);
        //        return country?.CountryName;
        //    }
        //    return null;
        //}
    }
}
