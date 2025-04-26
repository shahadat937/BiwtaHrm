using Hrm.Application.DTOs.ShiftSetting;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.ShiftSettings.Requests.Commands
{
    public class UpdateShiftSettingCommand : IRequest<BaseCommandResponse>  
    {
        public CreateShiftSettingDto ShiftSettingDto { get; set; }
    }
}
