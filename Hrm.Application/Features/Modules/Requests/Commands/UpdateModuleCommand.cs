using Hrm.Application.DTOs.Modules;
using Hrm.Application.Responses;
using MediatR;


namespace Hrm.Application.Features.Modules.Requests.Commands
{
    public class UpdateModuleCommand : IRequest<BaseCommandResponse>  
    {
        public CreateModuleDto ModuleDto { get; set; }
    }
}
