using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.NavbarSettings.Requests.Commands
{
    public class DeleteNavbarSettingCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
