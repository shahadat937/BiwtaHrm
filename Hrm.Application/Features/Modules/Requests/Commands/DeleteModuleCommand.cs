using MediatR;

namespace Hrm.Application.Features.Modules.Requests.Commands
{
    public class DeleteModuleCommand : IRequest  
    {  
        public int Id { get; set; }
    }
}
