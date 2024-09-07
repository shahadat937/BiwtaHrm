using Hrm.Application;
using Hrm.Application.DTOs.Section;
using Hrm.Application.Features.Section.Requests.Commands;
using Hrm.Application.Features.Section.Requests.Queries;
using Hrm.Application.Features.Stores.Requests.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hrm.Application.Features.BloodGroups.Requests.Queries;
using Hrm.Shared.Models;
using Hrm.Domain;
using Hrm.Application.DTOs.Designation;
using Hrm.Application.Features.Designation.Requests.Queries;

namespace Hrm.Api.Controllers
{
    [Route(HrmRoutePrefix.Section)]
    [ApiController]
    public class Section : Controller
    {
        private readonly IMediator _mediator;
        public Section(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("save-Section")]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateSectionDto Section)
        {
            var command = new CreateSectionCommand { SectionDto = Section };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpGet]
        [Route("get-Section")]
        public async Task<ActionResult> Get()
        {
            var Section = await _mediator.Send(new GetSectionRequest { });
            return Ok(Section);
        }

        [HttpGet]
        [Route("get-Sectionbyid/{id}")]
        public async Task<ActionResult<SectionDto>> Get(int id)
        {
            var Section = await _mediator.Send(new GetSectionByIdRequest { SectionId = id });
            return Ok(Section);

        }

        [HttpGet]
        [Route("get-selectedSection")]
        public async Task<ActionResult<List<SelectedModel>>> GetSelectedSection()
        {
            var Section = await _mediator.Send(new GetSelectedSectionRequest { });
            return Ok(Section);
        }

        [HttpPut]
        [Route("update-Section/{id}")]
        public async Task<ActionResult> Put([FromBody] SectionDto Section)
        {
            var command = new UpdateSectionCommand { SectionDto = Section };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete]
        [Route("delete-Section/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteSectionCommand { SectionId = id };
            var response = await _mediator.Send(command);
            return Ok(response);
        }


        [HttpGet]
        [Route("get-sectionByOfficeIdAndDepartmentId")]
        public async Task<ActionResult<DesignationDto>> GetSectionByOfficeIdAndDepartmentId(int departmentId)
        {
            var Designations = await _mediator.Send(new GetSelectedSectionByOfficeDepartmentRequest { DepartmentId = departmentId });
            return Ok(Designations);
        }
    }
}
