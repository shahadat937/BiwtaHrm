using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.OfficeOrder;


namespace Hrm.Application.Features.OfficeOrders.Requests.Commands
{
    public class CreateOfficeOrderCommand : IRequest<BaseCommandResponse> 
    {
        public CreateOfficeOrderDto OfficeOrderDto { get; set; }

    }
}
