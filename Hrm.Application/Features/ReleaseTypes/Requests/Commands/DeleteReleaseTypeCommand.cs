using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.ReleaseTypes.Requests.Commands
{
    public class DeleteReleaseTypeCommand : IRequest<BaseCommandResponse>
    {
        public int ReleaseTypeId { get; set; }
    }
}
