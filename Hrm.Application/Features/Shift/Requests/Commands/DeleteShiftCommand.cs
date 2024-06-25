using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteShiftCommand : IRequest<BaseCommandResponse>
    {
        public int ShiftId { get; set; }
    }
}
