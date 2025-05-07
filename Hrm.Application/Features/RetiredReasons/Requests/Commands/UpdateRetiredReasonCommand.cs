using Hrm.Application.DTOs.RetiredReason;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.RetiredReasons.Requests.Commands
{
    public class UpdateRetiredReasonCommand : IRequest<BaseCommandResponse>  
    {
        public CreateRetiredReasonDto RetiredReasonDto { get; set; }
    }
}
