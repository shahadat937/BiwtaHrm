using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.NavbarSetting;


namespace Hrm.Application.Features.NavbarSettings.Requests.Commands
{
    public class CreateNavbarSettingCommand : IRequest<BaseCommandResponse> 
    {
        public CreateNavbarSettingDto NavbarSettingDto { get; set; }

    }
}
