using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.TransferApproveInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.TransferApproveInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Queries
{
    public class GetTransferApproveInfoByIdRequestHandler : IRequestHandler<GetTransferApproveInfoByIdRequest, TransferApproveInfoDto>
    {

        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;
        private readonly IMapper _mapper;
        public GetTransferApproveInfoByIdRequestHandler(IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepositoy, IMapper mapper)
        {
            _TransferApproveInfoRepository = TransferApproveInfoRepositoy;
            _mapper = mapper;
        }

        public async Task<TransferApproveInfoDto> Handle(GetTransferApproveInfoByIdRequest request, CancellationToken cancellationToken)
        {
            var TransferApproveInfo = await _TransferApproveInfoRepository.Get(request.TransferApproveInfoId);
            return _mapper.Map<TransferApproveInfoDto>(TransferApproveInfo);
        }
    }
}
