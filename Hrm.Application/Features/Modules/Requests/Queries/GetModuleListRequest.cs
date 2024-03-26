using MediatR;
using Hrm.Application.DTOs.Common;
using Hrm.Application.Models;
using Hrm.Application.DTOs.Modules;

namespace Hrm.Application.Features.Modules.Requests.Queries
{
    public class GetModuleListRequest : IRequest<PagedResult<ModuleDto>>
    {
        public QueryParams QueryParams { get; set; }
    }
}
