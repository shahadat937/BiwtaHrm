using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteShiftCommand : IRequest
    {
        public int ShiftId { get; set; }
    }
}
