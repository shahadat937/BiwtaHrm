using Hrm.Application.DTOs.OrderType;
using MediatR;


namespace Hrm.Application.Features.OrderTypes.Requests.Queries
{
    public class GetOrderTypeDetailRequest : IRequest<OrderTypeDto>
    {
        public int Id { get; set; }
    }
}
