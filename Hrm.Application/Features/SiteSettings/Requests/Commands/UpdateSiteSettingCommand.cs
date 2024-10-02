using Hrm.Application.DTOs.SiteSetting;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.SiteSettings.Requests.Commands
{
    public class UpdateSiteSettingCommand : IRequest<BaseCommandResponse>  
    {
        public CreateSiteSettingDto SiteSettingDto { get; set; }
    }
}
