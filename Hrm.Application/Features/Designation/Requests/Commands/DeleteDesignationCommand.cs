using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteDesignationCommand : IRequest<BaseCommandResponse>
    {
        public int DesignationId { get; set; }
    }
}
