using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.SiteSettings.Requests.Commands
{
    public class DeleteSiteSettingCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
