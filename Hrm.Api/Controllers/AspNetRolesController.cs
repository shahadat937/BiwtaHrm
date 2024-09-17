using Hrm.Application;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.DTOs.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.AspNetRoles)]
    [ApiController]
    public class AspNetRolesController : ControllerBase
    {
        private readonly IRoleService _authenticationService;
        private readonly IMediator _mediator;
        public AspNetRolesController(IRoleService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("get-role")]
        public async Task<ActionResult<object>> GetRole()
        {
            return Ok(await _authenticationService.Get());
        }

        [HttpPost]
        [Route("save-role")]
        public async Task<ActionResult<BaseCommandResponse>> RoleCreate(CreateRoleDto request)
        {
            return Ok(await _authenticationService.Save(request));
        }


        [HttpPut]
        [Route("update-role/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> RoleUpdate(AspNetRolesDto model)
        {
            return Ok(await _authenticationService.Update(model));
        }

        [HttpDelete]
        [Route("delete-role/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> RoleDelete(string id)
        {
            return Ok(await _authenticationService.Delete(id));
        }

        [HttpGet]
        [Route("get-RoleById")]
        public async Task<ActionResult<BaseCommandResponse>> GetById(string id)
        {
            return Ok(await _authenticationService.GetById(id));
        }


    }
}
