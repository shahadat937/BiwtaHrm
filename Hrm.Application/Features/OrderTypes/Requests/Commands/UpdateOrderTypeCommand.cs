using Hrm.Application.DTOs.OrderType;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.OrderTypes.Requests.Commands
{
    public class UpdateOrderTypeCommand : IRequest<BaseCommandResponse>  
    {
        public CreateOrderTypeDto OrderTypeDto { get; set; }
    }
}
