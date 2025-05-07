using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.RetiredReasons.Requests.Commands
{
    public class DeleteRetiredReasonCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
