using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.Stores.Requests.Commands
{
    public class DeleteSectionCommand : IRequest<BaseCommandResponse>
    {
        public int SectionId { get; set; }
    }
}
