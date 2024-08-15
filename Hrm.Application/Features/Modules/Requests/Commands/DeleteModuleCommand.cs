using Hrm.Application.Responses;
using MediatR;

namespace Hrm.Application.Features.Modules.Requests.Commands
{
    public class DeleteModuleCommand : IRequest<BaseCommandResponse>  
    {  
        public int Id { get; set; }
    }
}
