using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteReligionCommand : IRequest
    {
        public int ReligionId { get; set; }
    }
}
