using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.ShiftSetting;


namespace Hrm.Application.Features.ShiftSettings.Requests.Commands
{
    public class CreateShiftSettingCommand : IRequest<BaseCommandResponse> 
    {
        public CreateShiftSettingDto ShiftSettingDto { get; set; }

    }
}
