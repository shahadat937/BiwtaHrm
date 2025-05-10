using Hrm.Application.Contracts.Persistence;
using Hrm.Application.Features.OrderTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OrderTypes.Handlers.Queries
{ 
    public class GetSelectedOrderTypeRequestHandler : IRequestHandler<GetSelectedOrderTypeRequest, List<SelectedModel>>
    {
        private readonly IHrmRepository<Hrm.Domain.OrderType> _OrderTypeRepository;


        public GetSelectedOrderTypeRequestHandler(IHrmRepository<Hrm.Domain.OrderType> OrderTypeRepository)
        {
            _OrderTypeRepository = OrderTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOrderTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<Hrm.Domain.OrderType> OrderTypes = await _OrderTypeRepository.FilterAsync(x => x.IsActive == true);
            List<SelectedModel> selectModels = OrderTypes.Select(x => new SelectedModel 
            {
                Name = x.TypeName,
                Id = x.Id
            }).ToList();
            return selectModels;
        }
    }
}
 