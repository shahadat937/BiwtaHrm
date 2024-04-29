using Hrm.Application;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.DTOs.Branch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.Branch.Requests.Commands;
using Hrm.Application.Features.Branch.Requests.Queries;
using Hrm.Application.Features.District.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Hrm.Application.Responses;
using Hrm.Domain;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.OfficeBranch)]
    [ApiController]
    public class OfficeBranchController : Controller
    {
        private readonly IMediator _mediator;
        public OfficeBranchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("get-officeBranch")]
        public async Task<ActionResult> GetBranch()
        {
            var Branch = await _mediator.Send(new GetBranchRequest { });
            return Ok(Branch);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-officeBranch")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateBranchDto Branch)
        {
            var command = new CreateBranchCommand { BranchDto = Branch };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        [Route("update-officeBranch/{id}")]
        public async Task<ActionResult> Put([FromBody] BranchDto Branch)
        {
            var command = new UpdateBranchCommand { BranchDto = Branch };
            var response = await _mediator.Send(command);
            return Ok(response);

             
           
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-officeBranch/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBranchCommand { BranchId = id };
            await _mediator.Send(command);
            return NoContent();
        }



        [HttpGet]
        [Route("get-officeBranchid/{id}")]
        public async Task<ActionResult<BranchDto>> Get(int id)
        {
            var Branch = await _mediator.Send(new GetBranchByIdRequest { BranchId = id });
            return Ok(Branch);

        }

        [HttpGet]
        [Route("get-selectedOfficeBranch")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedBranch()
        {
            var branch = await _mediator.Send(new GetSelectedBranchRequest { });
            return Ok(branch);
        }

    }
}
