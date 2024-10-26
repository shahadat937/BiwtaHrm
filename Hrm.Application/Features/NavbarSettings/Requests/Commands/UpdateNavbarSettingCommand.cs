using Hrm.Application.DTOs.NavbarSetting;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.NavbarSettings.Requests.Commands
{
    public class UpdateNavbarSettingCommand : IRequest<BaseCommandResponse>  
    {
        public CreateNavbarSettingDto NavbarSettingDto { get; set; }
    }
}
