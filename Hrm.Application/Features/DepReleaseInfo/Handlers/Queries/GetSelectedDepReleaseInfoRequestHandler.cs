using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.DepReleaseInfo.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.DepReleaseInfo.Handlers.Queries
{ 
    public class GetSelectedDepReleaseInfoRequestHandler : IRequestHandler<GetSelectedDepReleaseInfoRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.DepReleaseInfo> _DepReleaseInfoRepository;


        public GetSelectedDepReleaseInfoRequestHandler(IHrmRepository<Hrm.Domain.DepReleaseInfo> DepReleaseInfoRepository)
        {
            _DepReleaseInfoRepository = DepReleaseInfoRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDepReleaseInfoRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.DepReleaseInfo> DepReleaseInfos = await _DepReleaseInfoRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = DepReleaseInfos.Select(x => new SelectedModel 
            {
                Name = x.DepReleaseInfoName,
                Id = x.DepReleaseInfoId
            }).ToList();
            return selectModels;
        }
    }
}
 