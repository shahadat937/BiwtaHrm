using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteBranchCommand : IRequest
    {
        public int BranchId { get; set; }
    }
}
