using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteOfficeAddressCommand : IRequest
    {
        public int OfficeAddressId { get; set; }
    }
}
