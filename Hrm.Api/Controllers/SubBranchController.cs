using Hrm.Application;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.DTOs.MaritalStatus;
using Hrm.Application.Features.SubBranch.Requests.Commands;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Application.Features.SubBranch.Requests.Queries;
using Hrm.Application.Features.MaritalStatus.Requests.Commands;
using Hrm.Application.Features.MaritalStatus.Requests.Queries;
using Hrm.Application.Responses;
using Hrm.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.DTOs.SubBranch;
using Hrm.Application.Features.SubBranch.Requests.Queries;
namespace Hrm.Api.Controllers
{

    [Route(HrmRoutePrefix.SubBranch)]
    [ApiController]
    public class SubBranchController : Controller
    {
        private readonly IMediator _mediator;
        public SubBranchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-subBranch")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSubBranchDto SubBranch)
        {
            var command = new CreateSubBranchCommand { SubBranchDto = SubBranch };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        [Route("get-subBranch")]
        public async Task<ActionResult> Get()
        {
            var SubBranch = await _mediator.Send(new GetSubBranchRequest { });
            return Ok(SubBranch);
        }
        [HttpGet]
        [Route("get-SubBranchByOfficeBranchId/{id}")]
        public async Task<ActionResult<List<SubBranchDto>>> GetByOfficeBranchId(int id)
        {
            //var SubBranch = await _mediator.Send(new GetSubBranchByCountryIdRequest { CountryId = id });
            //return Ok(SubBranch);
            var SubBranchsByOfficeBranchId = await _mediator.Send(new GetSubBranchByOfficeBranchIdRequest
            {
                OfficeBranchId = id
            });
            return Ok(SubBranchsByOfficeBranchId);

        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("update-subBranch/{id}")]
        public async Task<ActionResult> Put([FromBody] SubBranchDto SubBranch)
        {
            var command = new UpdateSubBranchCommand { SubBranchDto = SubBranch };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        [Route("delete-subBranch/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSubBranchCommand { SubBranchId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-subBranchbyid/{id}")]
        public async Task<ActionResult<SubBranchDto>> Get(int id)
        {
            var SubBranch = await _mediator.Send(new GetSubBranchByIdRequest { SubBranchId = id });
            return Ok(SubBranch);

        }

        [HttpGet]
        [Route("get-selectedsubBranch")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedSubBranch()
        {
            var subBranch = await _mediator.Send(new GetSelectedSubBranchRequest { });
            return Ok(subBranch);
        }

    }
}
