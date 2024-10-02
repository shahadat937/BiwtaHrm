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
using Hrm.Application.Contracts.Identity;
using Hrm.Application.Models.Identity;
using Hrm.Application.DTOs.Role;
using Hrm.Application.DTOs.AspNetUserRoles;
using Microsoft.AspNetCore.Authorization;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.UserRole)]
    [ApiController]
    [Authorize]
    public class UserRoleController : Controller
    {
        private readonly IRoleService _authenticationService;
        private readonly IMediator _mediator;
        public UserRoleController(IRoleService authenticationService, IMediator mediator)
        {
            _authenticationService = authenticationService;
            _mediator = mediator;
        }

        //[HttpPost]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[Route("save-userRole")]
        //public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateUserRoleDto UserRole)
        //{
        //    var command = new CreateBloodCommand { UserRoleDto = UserRole };
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}


        [HttpPost]
        [Route("save-role")]
        public async Task<ActionResult<BaseCommandResponse>> RoleCreate(AspNetRolesDto request)
        {
            return Ok(await _authenticationService.Save(request));
        }



        [HttpGet]
        [Route("get-userRole")]
        public async Task<ActionResult> Get()
        {
            var UserRole = await _mediator.Send(new GetUserRoleRequest { });
            return Ok(UserRole);
        }


        //*************** AspNetUserRole *****************

        [HttpGet]
        [Route("get-userRoleDetail/{id}")]
        public async Task<ActionResult<AspNetUserRolesDto>> Get(string id)
        {
            var UserRoles = await _mediator.Send(new GetUserRoleDetailRequest { UserId = id });
            return UserRoles;
        }
        [HttpGet]
        [Route("get-selectedUserRoles")]
        public async Task<ActionResult<List<SelectedStringModel>>> GetSelectedUserRole()
        {
            var UserRole = await _mediator.Send(new GetSelectedUserRoleRequest { });
            return UserRole;
        }

        [HttpPut]
        [ProducesDefaultResponseType]
        [Route("update-userRole/{id}")]
        public async Task<ActionResult> Put([FromBody] AspNetUserRolesDto UserRole)
        {
            var command = new UpdateUserRoleCommand { UserRoleDto = UserRole };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //*************** AspNetUserRole *****************


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
