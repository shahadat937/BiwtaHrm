using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.OrderType;


namespace Hrm.Application.Features.OrderTypes.Requests.Commands
{
    public class CreateOrderTypeCommand : IRequest<BaseCommandResponse> 
    {
        public CreateOrderTypeDto OrderTypeDto { get; set; }

    }
}
