using Hrm.Application.DTOs.ShiftType;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.ShiftTypes.Requests.Commands
{
    public class UpdateShiftTypeCommand : IRequest<BaseCommandResponse>  
    {
        public CreateShiftTypeDto ShiftTypeDto { get; set; }
    }
}
