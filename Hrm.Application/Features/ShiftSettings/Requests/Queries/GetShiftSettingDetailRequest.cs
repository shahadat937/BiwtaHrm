using Hrm.Application.DTOs.ShiftSetting;
using MediatR;


namespace Hrm.Application.Features.ShiftSettings.Requests.Queries
{
    public class GetShiftSettingDetailRequest : IRequest<ShiftSettingDto>
    {
        public int Id { get; set; }
    }
}
