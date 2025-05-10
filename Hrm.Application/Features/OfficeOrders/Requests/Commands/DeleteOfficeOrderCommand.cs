using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.OfficeOrders.Requests.Commands
{
    public class DeleteOfficeOrderCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
