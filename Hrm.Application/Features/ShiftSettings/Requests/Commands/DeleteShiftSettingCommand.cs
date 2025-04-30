using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.ShiftSettings.Requests.Commands
{
    public class DeleteShiftSettingCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
