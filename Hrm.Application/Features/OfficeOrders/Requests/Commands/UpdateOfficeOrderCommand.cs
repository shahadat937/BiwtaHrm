using Hrm.Application.DTOs.OfficeOrder;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.OfficeOrders.Requests.Commands
{
    public class UpdateOfficeOrderCommand : IRequest<BaseCommandResponse>  
    {
        public CreateOfficeOrderDto OfficeOrderDto { get; set; }
    }
}
