using AutoMapper;
using Hrm.Application;
using Hrm.Application.DTOs.RoleDashboard;
using Hrm.Application.DTOs.RoleFeatures;
using Hrm.Application.Features.RoleDashboards.Requests.Commands;
using Hrm.Application.Features.RoleDashboards.Requests.Queries;
using Hrm.Application.Features.RoleFeatures.Requests.Commands;
using Hrm.Application.Features.RoleFeatures.Requests.Queries;
using Hrm.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.RoleDashboard)]
    [ApiController]
    [Authorize]
    public class RoleDashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly HrmDbContext _context;
        private readonly IMapper _mapper;

        public RoleDashboardController(IMediator mediator, HrmDbContext context, IMapper mapper)
        {
            _mediator = mediator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route(("get-allRoleDashboard"))]
        public async Task<ActionResult> GetAll()
        {
            var roleDashboard = await _mediator.Send(new GetRoleDashboardListRequest { });
            return Ok(roleDashboard);
        }


        [HttpGet]
        [Route(("get-roleDashboardPermission/{roleName}"))]
        public async Task<ActionResult> GetPermission(string roleName)
        {
            var roleDashboard = await _mediator.Send(new GetRoleDashboardPermissionByRoleRequest { RoleName = roleName });
            return Ok(roleDashboard);
        }


        [HttpPost]
        [Route("save-roleDashboard")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] List<CreateRoleDashboardDto> roleDashboardDtos)
        {
            var command = new CreateRoleDashboardCommand { RoleDashboardDtos = roleDashboardDtos };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
