using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.TransferApproveInfo.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.TransferApproveInfo.Handlers.Queries
{ 
    public class GetSelectedTransferApproveInfoRequestHandler : IRequestHandler<GetSelectedTransferApproveInfoRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.TransferApproveInfo> _TransferApproveInfoRepository;


        public GetSelectedTransferApproveInfoRequestHandler(IHrmRepository<Hrm.Domain.TransferApproveInfo> TransferApproveInfoRepository)
        {
            _TransferApproveInfoRepository = TransferApproveInfoRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedTransferApproveInfoRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.TransferApproveInfo> TransferApproveInfos = await _TransferApproveInfoRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = TransferApproveInfos.Select(x => new SelectedModel 
            {
                Name = x.TransferApproveInfoName,
                Id = x.TransferApproveInfoId
            }).ToList();
            return selectModels;
        }
    }
}
 