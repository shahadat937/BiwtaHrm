using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteDesignationCommand : IRequest
    {
        public int DesignationId { get; set; }
    }
}
