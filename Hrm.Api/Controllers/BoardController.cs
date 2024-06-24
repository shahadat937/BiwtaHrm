using Hrm.Application;
using Hrm.Application.DTOs.Board;
using Hrm.Application.Features.Board.Requests.Commands;
using Hrm.Application.Features.Board.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Board)]
    [ApiController]
    public class Board : Controller
    {
        private readonly IMediator _mediator;
        public Board(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Board")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBoardDto Board)
        {
            var command = new CreateBoardCommand { BoardDto = Board };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-Board")]
        public async Task<ActionResult> Get()
        {
            var Board = await _mediator.Send(new GetBoardRequest { });
            return Ok(Board);
        }

        [HttpGet]
        [Route("get-Boardbyid/{id}")]
        public async Task<ActionResult<BoardDto>> Get(int id)
        {
            var Board = await _mediator.Send(new GetBoardByIdRequest { BoardId = id });
            return Ok(Board);

        }

        [HttpGet]
        [Route("get-selectedBoard")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBoard()
        {
            var Board = await _mediator.Send(new GetSelectedBoardRequest { });
            return Ok(Board);
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-Board/{id}")]
        public async Task<ActionResult> Put([FromBody] BoardDto Board)
        {
            var command = new UpdateExamTypeCommand { BoardDto = Board };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-Board/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBoardCommand { BoardId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
