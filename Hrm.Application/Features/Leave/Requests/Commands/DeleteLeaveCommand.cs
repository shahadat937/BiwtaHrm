using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteLeaveCommand : IRequest
    {
        public int LeaveId { get; set; }
    }
}
