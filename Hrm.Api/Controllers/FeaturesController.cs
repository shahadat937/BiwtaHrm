using Hrm.Application;
using Hrm.Application.DTOs.Features;
using Hrm.Application.Enum;
using Hrm.Application.Features.Features.Requests.Commands;
using Hrm.Application.Features.Features.Requests.Queries;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Feature)]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FeaturesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-Features")]
        public async Task<ActionResult<List<FeatureDto>>> Get()
        {
            var Features = await _mediator.Send(new GetFeatureListRequest { });
            return Ok(Features);
        }

        [HttpGet]
        [Route("get-FeatureDetail/{id}")]
        public async Task<ActionResult<FeatureDto>> Get(int id)
        {
            var Feature = await _mediator.Send(new GetFeatureDetailRequest { Id = id });
            return Ok(Feature);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Feature")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateFeatureDto Feature)
        {
            var command = new CreateFeatureCommand { FeatureDto = Feature };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-Feature/{id}")]
        public async Task<ActionResult> Put([FromBody] CreateFeatureDto Feature)
        {
            var command = new UpdateFeatureCommand { FeatureDto = Feature };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("delete-Feature/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteFeatureCommand { Id = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-selectedFeatures")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedFeature()
        {
            var featureByFeature = await _mediator.Send(new GetSelectedFeatureRequests { });
            return Ok(featureByFeature);
        }


    }
}