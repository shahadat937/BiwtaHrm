using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteGroupCommand : IRequest
    {
        public int GroupId { get; set; }
    }
}
