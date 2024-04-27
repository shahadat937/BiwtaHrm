using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteBoardCommand : IRequest
    {
        public int BoardId { get; set; }
    }
}
