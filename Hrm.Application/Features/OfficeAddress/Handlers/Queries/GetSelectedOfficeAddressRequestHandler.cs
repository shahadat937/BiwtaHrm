using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OfficeAddress.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OfficeAddress.Handlers.Queries
{ 
    public class GetSelectedOfficeAddressRequestHandler : IRequestHandler<GetSelectedOfficeAddressRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.OfficeAddress> _OfficeAddressRepository;


        public GetSelectedOfficeAddressRequestHandler(IHrmRepository<Hrm.Domain.OfficeAddress> OfficeAddressRepository)
        {
            _OfficeAddressRepository = OfficeAddressRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOfficeAddressRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.OfficeAddress> OfficeAddresss = await _OfficeAddressRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = OfficeAddresss.Select(x => new SelectedModel 
            {
                Name = x.OfficeAddressName,
                Id = x.OfficeAddressId
            }).ToList();
            return selectModels;
        }
    }
}
 