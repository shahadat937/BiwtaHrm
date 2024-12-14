using Hrm.Application;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.DTOs.BloodGroup;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Application.Models.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Hrm.Api.Controllers;
//[Route(HrmRoutePrefix.Account)]
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly IMediator _mediator;
    public AccountController(IAuthService authenticationService, IMediator mediator)
    {
        _authenticationService = authenticationService;
        _mediator = mediator;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }

    [HttpPost]
    [Route("verifyToken")]
    public async Task<ActionResult<BaseCommandResponse>> VerifyToken([FromBody]VerifyTokenRequest request)
    {
        return Ok(await _authenticationService.VerifyToken(request));
    }

}

