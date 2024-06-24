using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteOfficeCommand : IRequest
    {
        public int OfficeId { get; set; }
    }
}
