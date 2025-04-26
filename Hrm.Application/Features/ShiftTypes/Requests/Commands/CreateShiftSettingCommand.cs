using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.ShiftType;


namespace Hrm.Application.Features.ShiftTypes.Requests.Commands
{
    public class CreateShiftTypeCommand : IRequest<BaseCommandResponse> 
    {
        public CreateShiftTypeDto ShiftTypeDto { get; set; }

    }
}
