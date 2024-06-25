using Hrm.Application.DTOs.Modules;
using MediatR;


namespace Hrm.Application.Features.Modules.Requests.Commands
{
    public class UpdateModuleCommand : IRequest<Unit>  
    {
        public ModuleDto ModuleDto { get; set; }
    }
}
