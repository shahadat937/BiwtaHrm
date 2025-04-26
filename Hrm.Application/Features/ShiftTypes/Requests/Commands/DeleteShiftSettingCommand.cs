using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.ShiftTypes.Requests.Commands
{
    public class DeleteShiftTypeCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
