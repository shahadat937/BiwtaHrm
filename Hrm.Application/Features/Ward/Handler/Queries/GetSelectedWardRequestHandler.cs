using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.Ward.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.Ward.Handlers.Queries
{ 
    public class GetSelectedWardRequestHandler : IRequestHandler<GetSelectedWardRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.Ward> _WardRepository;


        public GetSelectedWardRequestHandler(IHrmRepository<Hrm.Domain.Ward> WardRepository)
        {
            _WardRepository = WardRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedWardRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.Ward> Wards = await _WardRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Wards.Select(x => new SelectedModel 
            {
                Name = x.WardName,
                Id = x.WardId
            }).ToList();
            return selectModels;
        }
    }
}
 