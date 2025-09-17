using Hrm.Application.DTOs.PostingType;
using Hrm.Application;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.PostingTypes.Request.Queries;
using Hrm.Application.Features.PostingTypes.Request.Commands;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.PostingType)]
    [ApiController]
    [Authorize]
    public class PostingTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PostingTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-PostingTypes")]
        public async Task<ActionResult<List<PostingTypeDto>>> Get()
        {
            var PostingTypes = await _mediator.Send(new GetPostingTypeRequest { });
            return Ok(PostingTypes);
        }


        [HttpGet]
        [Route("get-selectedPostingTypes")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedPostingType()
        {
            var PostingTypes = await _mediator.Send(new GetSelectedPostingTypeRequest { });
            return Ok(PostingTypes);
        }


        [HttpGet]
        [Route("get-PostingTypeDetail/{id}")]
        public async Task<ActionResult<PostingTypeDto>> Get(int id)
        {
            var PostingType = await _mediator.Send(new GetPostingTypeDetailRequest { PostingTypeId = id });
            return Ok(PostingType);
        }

        [HttpPost]
        [Route("save-PostingType")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePostingTypeDto PostingType)
        {
            var command = new CreatePostingTypeCommand { PostingTypeDto = PostingType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [Route("update-PostingType/{id}")]
        public async Task<ActionResult> Put([FromBody] PostingTypeDto PostingType)
        {
            var command = new UpdatePostingTypeCommand { PostingTypeDto = PostingType };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesDefaultResponseType]
        [Route("delete-PostingType/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePostingTypeCommand { PostingTypeId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


    }

}