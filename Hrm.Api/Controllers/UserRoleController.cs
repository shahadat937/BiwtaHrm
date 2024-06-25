using Hrm.Application;
using Hrm.Application.DTOs.UserRole;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.UserRole.Requests.Commands;
using Hrm.Application.Features.UserRole.Requests.Queries;
using Hrm.Application.Features.UserRoles.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.UserRole)]
    [ApiController]
    public class UserRoleController : Controller
    {
        private readonly IMediator _mediator;
        public UserRoleController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-userRole")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUserRoleDto UserRole)
        {
            var command = new CreateBloodCommand { UserRoleDto = UserRole };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-userRole")]
        public async Task<ActionResult> Get()
        {
            var UserRole = await _mediator.Send(new GetUserRoleRequest { });
            return Ok(UserRole);
        }
        [HttpGet]
        [Route("get-userRoleDetail/{id}")]
        public async Task<ActionResult<UserRoleDto>> Get(int id)
        {
            var UserRoles = await _mediator.Send(new GetUserRoleDetailRequest { UserRoleId = id });
            return Ok(UserRoles);
        }
        [HttpGet]
        [Route("get-selectedUserRoles")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedUserRole()
        {
            var UserRole = await _mediator.Send(new GetSelectedUserRoleRequest { });
            return Ok(UserRole);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-userRole/{id}")]
        public async Task<ActionResult> Put([FromBody] UserRoleDto UserRole)
        {
            var command = new UpdateUserRoleCommand { UserRoleDto = UserRole };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-UserRole/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserRoleCommand { UserRoleId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
