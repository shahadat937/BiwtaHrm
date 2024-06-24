using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteDistrictCommand : IRequest
    {
        public int DistrictId { get; set; }
    }
}
