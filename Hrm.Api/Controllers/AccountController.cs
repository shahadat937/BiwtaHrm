using Hrm.Application;
using Hrm.Application.Contracts.Identity;
using Hrm.Application.Models.Identity;

namespace Hrm.Api.Controllers;
[Route(HrmRoutePrefix.Account)]
//[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    public AccountController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
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
}

