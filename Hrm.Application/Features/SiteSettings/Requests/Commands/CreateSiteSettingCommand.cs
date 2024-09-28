using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.SiteSetting;


namespace Hrm.Application.Features.SiteSettings.Requests.Commands
{
    public class CreateSiteSettingCommand : IRequest<BaseCommandResponse> 
    {
        public CreateSiteSettingDto SiteSettingDto { get; set; }

    }
}
