using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TransferApproveInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.TransferApproveInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Queries
{
    public class GetTransferApproveInfoRequestHandler : IRequestHandler<GetTransferApproveInfoRequest, object>
    {

        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
        private readonly IHrmRepository<Hrm.Domain.Country> _countryRepository;
        private readonly IMapper _mapper;
        public GetTransferApproveInfoRequestHandler(IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository, IMapper mapper, IHrmRepository<Domain.Country> countryRepository)
        {
            _TransferApproveInfoRepository = TransferApproveInfoRepository;
            _mapper = mapper;
            _countryRepository = countryRepository;
        }

        public async Task<object> Handle(GetTransferApproveInfoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = _TransferApproveInfoRepository.FilterWithInclude(x => true);
            TransferApproveInfos = TransferApproveInfos.OrderByDescending(x => x.TransferApproveInfoId);

            var TransferApproveInfoDtos = new List<TransferApproveInfoDto>();

            foreach (var TransferApproveInfo in TransferApproveInfos)
            {
                var TransferApproveInfoDto = _mapper.Map<TransferApproveInfoDto>(TransferApproveInfo);
               // var countryName = await GetCountryName(TransferApproveInfo.CountryId);
             //   TransferApproveInfoDto.CountryName = countryName;
                TransferApproveInfoDtos.Add(TransferApproveInfoDto);
            }

            return TransferApproveInfoDtos;
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
