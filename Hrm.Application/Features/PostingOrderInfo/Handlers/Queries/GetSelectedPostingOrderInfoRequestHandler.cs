using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.PostingOrderInfo.Handlers.Queries
{ 
    public class GetSelectedPostingOrderInfoRequestHandler : IRequestHandler<GetSelectedPostingOrderInfoRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.PostingOrderInfo> _PostingOrderInfoRepository;


        public GetSelectedPostingOrderInfoRequestHandler(IHrmRepository<Hrm.Domain.PostingOrderInfo> PostingOrderInfoRepository)
        {
            _PostingOrderInfoRepository = PostingOrderInfoRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPostingOrderInfoRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.PostingOrderInfo> PostingOrderInfos = await _PostingOrderInfoRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = PostingOrderInfos.Select(x => new SelectedModel 
            {
               // Name = x.PostingOrderInfoName,
                Id = x.PostingOrderInfoId
            }).ToList();
            return selectModels;
        }
    }
}
 