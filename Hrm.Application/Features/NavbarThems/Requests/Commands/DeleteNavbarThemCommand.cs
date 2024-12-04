using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.NavbarThems.Requests.Commands
{
    public class DeleteNavbarThemCommand : IRequest<BaseCommandResponse>
    {
        public int NavbarThemId { get; set; }
    }
}
