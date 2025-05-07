using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.RetiredReason;


namespace Hrm.Application.Features.RetiredReasons.Requests.Commands
{
    public class CreateRetiredReasonCommand : IRequest<BaseCommandResponse> 
    {
        public CreateRetiredReasonDto RetiredReasonDto { get; set; }

    }
}
