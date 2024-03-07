using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteGenderCommand : IRequest
    {
        public int GenderId { get; set; }
    }
}
