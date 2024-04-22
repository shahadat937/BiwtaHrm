using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeletePoolCommand : IRequest
    {
        public int PoolId { get; set; }
    }
}
