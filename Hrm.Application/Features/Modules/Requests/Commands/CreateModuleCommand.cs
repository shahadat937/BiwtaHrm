using MediatR;
using Hrm.Application.Responses;
using Hrm.Application.DTOs.Modules;


namespace Hrm.Application.Features.Modules.Requests.Commands
{
    public class CreateModuleCommand : IRequest<BaseCommandResponse> 
    {
        public CreateModuleDto ModuleDto { get; set; }

    }
}
