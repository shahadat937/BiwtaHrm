using Hrm.Application;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.DTOs.AspNetUsers;
using Hrm.Application.Features.AspNetUsers.Requests.Commands;
using Hrm.Application.Features.AspNetUsers.Requests.Queries;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.AspNetUsers)]
    [ApiController]
    public class AspNetUsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthService _authenticationService;
        public AspNetUsersController(IMediator mediator, IAuthService authenticationService)
        {
            _mediator = mediator;
            _authenticationService = authenticationService;
        }


        [HttpGet]
        [Route("get-users")]
        public async Task<ActionResult> Get()
        {
            var Users = await _mediator.Send(new GetUserListRequest { });
            return Ok(Users);
        }

        [HttpGet]
        [Route("get-userById/{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var users = await _mediator.Send(new GetUserDetailsRequest { Id = id });
            return Ok(users);
        }

        //[HttpPost]
        //[Route("save-user")]
        //public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateAspNetUserDto userDto)
        //{
        //    var command = new CreateAspNetUserCommand { AspNetUserDto = userDto };
        //    var response = await _mediator.Send(command);
        //    return Ok(response);
        //}

        [HttpPut]
        [Route("update-user/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateUser(UpdateUserRequest request)
        {
            return Ok(await _authenticationService.UpdateUser(request));
        }

        [HttpPut]
        [Route("update-password/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateUserAndPassword(UpdateUserRequest request)
        {
            return Ok(await _authenticationService.UpdateUserAndChangePassword(request));
        }


        [HttpGet]
        [Route("get-userByEmpId/{id}")]
        public async Task<ActionResult> GetByEmpId(int id)
        {
            var users = await _mediator.Send(new GetUserDetailsByEmpIdRequest { Id = id });
            return Ok(users);
        }

    }
}
