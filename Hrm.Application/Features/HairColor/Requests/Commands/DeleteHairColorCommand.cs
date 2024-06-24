using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteHairColorCommand : IRequest
    {
        public int HairColorId { get; set; }
    }
}
