using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.ReleaseTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.ReleaseTypes.Handlers.Queries
{ 
    public class GetSelectedReleaseTypeRequestHandler : IRequestHandler<GetSelectedReleaseTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.ReleaseType> _ReleaseTypeRepository;


        public GetSelectedReleaseTypeRequestHandler(IHrmRepository<Hrm.Domain.ReleaseType> ReleaseTypeRepository)
        {
            _ReleaseTypeRepository = ReleaseTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReleaseTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.ReleaseType> ReleaseTypes = await _ReleaseTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = ReleaseTypes.Select(x => new SelectedModel 
            {
                Name = x.ReleaseTypeName,
                Id = x.ReleaseTypeId
            }).ToList();
            return selectModels;
        }
    }
}
 