using Hrm.Application;
using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.DTOs.PostingOrderInfo;
using Hrm.Application.Features.PostingOrderInfo.Requests.Commands;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.PostingOrderInfo.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Domain;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.PostingOrderInfo)]
    [ApiController]
    public class PostingOrderInfoController : Controller
    {
        private readonly IMediator _mediator;
        public PostingOrderInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-PostingOrderInfo")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreatePostingOrderInfoDto PostingOrderInfo)
        {
            var command = new CreatePostingOrderInfoCommand { PostingOrderInfoDto = PostingOrderInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-PostingOrderInfo")]
        public async Task<ActionResult> Get()
        {
            var PostingOrderInfo = await _mediator.Send(new GetPostingOrderInfoRequest { });
            return Ok(PostingOrderInfo);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-PostingOrderInfo/{id}")]
        public async Task<ActionResult> Put([FromBody] PostingOrderInfoDto PostingOrderInfo)
        {
            var command = new UpdatePostingOrderInfoCommand { PostingOrderInfoDto = PostingOrderInfo };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-PostingOrderInfo/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeletePostingOrderInfoCommand { PostingOrderInfoId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }



        [HttpGet]
        [Route("get-PostingOrderInfobyid/{id}")]
        public async Task<ActionResult<PostingOrderInfoDto>> Get(int id)
        {
            var PostingOrderInfo = await _mediator.Send(new GetPostingOrderInfoByIdRequest { PostingOrderInfoId = id });
            return Ok(PostingOrderInfo);

        }
        [HttpGet]
        [Route("get-PostingOrderInfoByCountryId/{id}")]
        public async Task<ActionResult<List<PostingOrderInfoDto>>> GetByCountryId(int id)
        {
            //var PostingOrderInfo = await _mediator.Send(new GetPostingOrderInfoByCountryIdRequest { CountryId = id });
            //return Ok(PostingOrderInfo);
            var PostingOrderInfosByCountryId = await _mediator.Send(new GetPostingOrderInfoByCountryIdRequest
            {
                CountryId = id
            });
            return Ok(PostingOrderInfosByCountryId);

        }
        [HttpGet]
        [Route("get-selectedPostingOrderInfo")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedPostingOrderInfo()
        {
            var PostingOrderInfo = await _mediator.Send(new GetSelectedPostingOrderInfoRequest { });
            return Ok(PostingOrderInfo);
        }
    }
}
