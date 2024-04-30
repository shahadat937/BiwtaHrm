using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.DepReleaseInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.DepReleaseInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Queries
{
    public class GetDepReleaseInfoByIdRequestHandler : IRequestHandler<GetDepReleaseInfoByIdRequest, DepReleaseInfoDto>
    {

        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;
        private readonly IMapper _mapper;
        public GetDepReleaseInfoByIdRequestHandler(IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepositoy, IMapper mapper)
        {
            _DepReleaseInfoRepository = DepReleaseInfoRepositoy;
            _mapper = mapper;
        }

        public async Task<DepReleaseInfoDto> Handle(GetDepReleaseInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var DepReleaseInfo = await _DepReleaseInfoRepository.Get(request.DepReleaseInfoId);
            return _mapper.Map<DepReleaseInfoDto>(DepReleaseInfo);
        }
    }
}
