using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteSectionCommand : IRequest
    {
        public int SectionId { get; set; }
    }
}
