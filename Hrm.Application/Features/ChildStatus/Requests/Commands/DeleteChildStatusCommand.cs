using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteChildStatusCommand : IRequest
    {
        public int ChildStatusId { get; set; }
    }
}
