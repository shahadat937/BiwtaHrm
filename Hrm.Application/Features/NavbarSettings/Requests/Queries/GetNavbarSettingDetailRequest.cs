using Hrm.Application.DTOs.NavbarSetting;
using MediatR;


namespace Hrm.Application.Features.NavbarSettings.Requests.Queries
{
    public class GetNavbarSettingDetailRequest : IRequest<NavbarSettingDto>
    {
        public int Id { get; set; }
    }
}
