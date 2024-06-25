

using Hrm.Application;
using Hrm.Application.DTOs.Modules;
using Hrm.Application.Enum;
using Hrm.Application.Features.Modules.Requests.Commands;
using Hrm.Application.Features.Modules.Requests.Queries;
using Hrm.Shared.Models;

namespace Hrm.Api.Controllers;

[Route(HrmRoutePrefix.Module)]
[Route("api/[controller]")]
[ApiController]
public class ModuleController : ControllerBase
{
    private readonly IMediator _mediator;
   
    public ModuleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("get-modules")]
    public async Task<ActionResult<List<ModuleDto>>> Get([FromQuery] QueryParams queryParams)
    {
        var Modules = await _mediator.Send(new GetModuleListRequest { QueryParams = queryParams });
        return Ok(Modules);
    }

    [HttpGet]
    [Route("get-moduleDetail/{id}")]
    public async Task<ActionResult<ModuleDto>> Get(int id)
    {
        var Module = await _mediator.Send(new GetModuleDetailRequest { Id = id });
        return Ok(Module);
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [Route("save-module")]
    public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateModuleDto Module)
    {
        var command = new CreateModuleCommand { ModuleDto = Module };
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("update-module/{id}")]
    public async Task<ActionResult> Put([FromBody] ModuleDto Module)
    {
        var command = new UpdateModuleCommand { ModuleDto = Module };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    [Route("delete-module/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new DeleteModuleCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpGet]
    [Route("get-selectedModules")]
    public async Task<ActionResult<List<SelectedModel>>> GetSelectedModule()
    {
        var featureByModule = await _mediator.Send(new GetSelectedModuleRequests { });
        return Ok(featureByModule);
    } 


    [HttpGet]
    [Route("get-module-features")]
    public async Task<ActionResult> GetModuleFeatures()
    {
        var featureByModule = await _mediator.Send(new GetModuleFeaturesRequests { FeatureType = ((int)FeatureType.Feature) });
        return Ok(featureByModule);
    }

}

