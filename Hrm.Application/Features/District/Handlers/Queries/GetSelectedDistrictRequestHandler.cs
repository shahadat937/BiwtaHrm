using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.District.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.District.Handlers.Queries
{ 
    public class GetSelectedDistrictRequestHandler : IRequestHandler<GetSelectedDistrictRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.District> _DistrictRepository;


        public GetSelectedDistrictRequestHandler(IHrmRepository<Hrm.Domain.District> DistrictRepository)
        {
            _DistrictRepository = DistrictRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDistrictRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.District> Districts = await _DistrictRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = Districts.Select(x => new SelectedModel 
            {
                Name = x.DistrictName,
                Id = x.DistrictId
            }).ToList();
            return selectModels;
        }
    }
}
 