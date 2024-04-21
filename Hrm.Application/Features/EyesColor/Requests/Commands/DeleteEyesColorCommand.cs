using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteEyesColorCommand : IRequest
    {
        public int EyesColorId { get; set; }
    }
}
