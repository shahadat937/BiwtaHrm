using Hrm.Application.DTOs.SiteSetting;
using MediatR;


namespace Hrm.Application.Features.SiteSettings.Requests.Queries
{
    public class GetSiteSettingDetailRequest : IRequest<SiteSettingDto>
    {
        public int Id { get; set; }
    }
}
