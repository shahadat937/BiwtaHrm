using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.OrderTypes.Requests.Commands
{
    public class DeleteOrderTypeCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
