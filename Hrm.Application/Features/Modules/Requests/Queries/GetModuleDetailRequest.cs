using Hrm.Application.DTOs.Modules;
using MediatR;


namespace Hrm.Application.Features.Modules.Requests.Queries
{
    public class GetModuleDetailRequest : IRequest<ModuleDto>
    {
        public int Id { get; set; }
    }
}
