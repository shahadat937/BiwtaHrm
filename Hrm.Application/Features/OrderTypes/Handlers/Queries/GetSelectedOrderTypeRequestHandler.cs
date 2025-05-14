using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Features.OrderTypes.Requests.Queries;
using Hrm.Shared.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Hrm.Application.Features.OrderTypes.Handlers.Queries
{ 
    public class GetSelectedOrderTypeRequestHandler : IRequestHandler<GetSelectedOrderTypeRequest, List<SelectedOrderTypeDto>>
    {
        private readonly IHrmRepository<Hrm.Domain.OrderType> _OrderTypeRepository;
        private readonly IHrmRepository<Hrm.Domain.OfficeOrder> _OfficeOrderRepository;


        public GetSelectedOrderTypeRequestHandler(IHrmRepository<Hrm.Domain.OrderType> OrderTypeRepository, IHrmRepository<Domain.OfficeOrder> officeOrderRepository)
        {
            _OrderTypeRepository = OrderTypeRepository;
            _OfficeOrderRepository = officeOrderRepository;
        }

        public async Task<List<SelectedOrderTypeDto>> Handle(GetSelectedOrderTypeRequest request, CancellationToken cancellationToken)
        {
            var orderTypes = await _OrderTypeRepository.FilterAsync(x => x.IsActive == true);

            var officeOrders = await _OfficeOrderRepository.GetAll();

            var orderTypeCounts = officeOrders
            .Where(o => o.OrderTypeId.HasValue)
            .GroupBy(o => o.OrderTypeId.Value)
            .ToDictionary(g => g.Key, g => g.Count());

            var result = orderTypes.Select(orderType => new SelectedOrderTypeDto
            {
                Id = orderType.Id,
                Name = orderType.TypeName,
                Count = orderTypeCounts.ContainsKey(orderType.Id) ? orderTypeCounts[orderType.Id] : 0,
                TotalCount = officeOrders.Count()
            }).OrderByDescending(x => x.Count).ToList();

            return result;


        }
    }
}
 