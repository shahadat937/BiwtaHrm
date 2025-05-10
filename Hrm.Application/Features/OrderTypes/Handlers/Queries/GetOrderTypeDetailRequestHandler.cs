using AutoMapper;
using Hrm.Application.Contracts.Persistence;
using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Features.OrderTypes.Requests.Queries;
using Hrm.Domain;
using MediatR;


namespace Hrm.Application.Features.OrderTypes.Handlers.Queries
{
    public class GetOrderTypesDetailRequestHandler : IRequestHandler<GetOrderTypeDetailRequest, OrderTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IHrmRepository<OrderType> _OrderTypeRepository;
        public GetOrderTypesDetailRequestHandler(IHrmRepository<OrderType> OrderTypeRepository, IMapper mapper)
        {
            _OrderTypeRepository = OrderTypeRepository;
            _mapper = mapper;
        }
        public async Task<OrderTypeDto> Handle(GetOrderTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var OrderType = await _OrderTypeRepository.Get(request.Id);
            return _mapper.Map<OrderTypeDto>(OrderType);
        }
    }
}
